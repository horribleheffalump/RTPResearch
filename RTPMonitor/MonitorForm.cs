using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using RTPClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTPMonitor
{
    public partial class MonitorForm : Form
    {
        public bool doRead = true;
        Dictionary<string, List<string>> TempSessions = new Dictionary<string, List<string>>();
        RTPSession session = new RTPSession();
        List<string> rawSessionData = new List<string>();
        string sessionId = "";
        ulong currentFrameNumber = 0;

        //double lastShownEstimationInstant = 0;

        RTPModel model = new RTPModel();

        //int ModelReidentificationFramesLimit = 1000;
        //int UnusedForIdentificationFramesCount = 0;
        //bool canEstimate = false;
        //int drawGap = 15;

        public MonitorForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            doRead = true;
            TempSessions = new Dictionary<string, List<string>>();
            session = new RTPSession();
            rawSessionData = new List<string>();
            sessionId = "";
            currentFrameNumber = 0;


            chart.ChartAreas[0].AxisY.Maximum = 0.3;
            chart.ChartAreas[0].AxisY.Minimum = -0.1;
            chart.ChartAreas[0].AxisY2.Maximum = 30;
            chart.ChartAreas[0].AxisY2.Minimum = -10;

        }


        private void backgroundWorkerReader_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = textBoxFileName.Text;
            using (FileStream fs = new FileStream
               (fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int waitingTime = 0;
                    int maxWaitingTime = 1200000;
                    while (!backgroundWorkerReader.CancellationPending)
                    {
                        while (!sr.EndOfStream)
                        {
                            backgroundWorkerReader.ReportProgress(0, sr.ReadLine());
                            if (backgroundWorkerReader.CancellationPending)
                                break;
                            Thread.Sleep(1);
                        }
                        while (sr.EndOfStream)
                        {
                            string s = sr.ReadLine();
                            if (s == null) s = "";
                            if (s.Length > 0)
                            {
                                backgroundWorkerReader.ReportProgress(0, s);
                            }
                            else
                            {
                                backgroundWorkerReader.ReportProgress(0, "End of stream " + waitingTime.ToString());
                                waitingTime += 100;
                                if (waitingTime > maxWaitingTime)
                                {
                                    session.Complete = true;
                                    backgroundWorkerReader.ReportProgress(0, "End of stream " + "SESSION COMPLETE");
                                    break; // если долго ничего не делаем (2 минуты в сумме), то выходим
                                }
                                Thread.Sleep(100);
                            }
                        }
                        if (session.Complete)
                            break;
                        //backgroundWorkerReader.ReportProgress(0, sr.ReadLine());
                    }
                }
            }
        }

        private void backgroundWorkerReader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string s = (string)e.UserState;

            if (!s.Contains("End of stream"))
            {
                if (checkBoxTSharkRTPData.Checked)
                {
                    ProcessTSharkString(s);
                }
                else
                {
                    ProcessLegacyString(s);
                }
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    textBoxTest.Text = "[" + textBoxTest.Lines.Count().ToString() + "]" + s + "\r\n" + textBoxTest.Text;
                }));
                Thread.Sleep(10);

            }

            //if (ProcessTSharkString(s))
            //{
            //    //textBoxOutput.AppendText(s);
            //    //textBoxOutput.AppendText("\r\n");
            //}
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            backgroundWorkerReader.CancelAsync();
            backgroundWorkerPainter.CancelAsync();
            //backgroundWorkerProcessor.CancelAsync();
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            model.Init(double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text), int.Parse(textBoxPC_b.Text));
            for (int i = 0; i < chart.Series.Count; i++)
            {
                chart.Series[i].Points.Clear();
            }

            backgroundWorkerReader.RunWorkerAsync();
            backgroundWorkerPainter.RunWorkerAsync();
            //backgroundWorkerProcessor.RunWorkerAsync();
            buttonStop.Enabled = true;
            buttonStart.Enabled = false;
            //textBoxOutput.Clear();
        }

        private bool ProcessTSharkString(string s)
        {
            bool added = false;
            s = s.TrimStart(' ').Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            if (s.Contains(textBoxFilter.Text.Replace("  ", " ")))
            {
                string[] values = s.Split(' ');
                if (values.Length > 10) // похоже, так теряются строки, которые прочли пока они не успели до конца записаться в лог. ПОДУМАТЬ!!!
                {
                    if (sessionId == "")
                    {
                        if (!TempSessions.ContainsKey(values[8]))
                        {
                            TempSessions.Add(values[8], new List<string>());
                        }
                        TempSessions[values[8]].Add(s);
                        if (values.Length > 11) // this session has a Marked packet => this is a video session, so take it!!
                        {
                            sessionId = values[8];
                            foreach (string tempstr in TempSessions[sessionId])
                            {
                                rawSessionData.Add(tempstr);
                                string[] tempvals = tempstr.Split(' ');
                                session.Add(RTPPacket.ImportTShark(tempvals));
                            }
                        }
                    }
                    else if (sessionId == values[8])
                    {
                        rawSessionData.Add(s);
                        RTPPacket p = RTPPacket.ImportTShark(values);
                        if (!p.Corrupted)
                            session.Add(p);
                        added = true;
                    }
                }
            }
            return added;
        }

        private void ProcessLegacyString(string s)
        {
            string[] values = s.Split(' ');
            session.Add(new RTPPacket(values));
        }

        private void backgroundWorkerPainter_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorkerPainter.CancellationPending)
            {
                if (currentFrameNumber < session.CurrentFrameNumber - session.WindowSize - 1)
                {
                    backgroundWorkerPainter.ReportProgress(0);
                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Sleep(100);
                }
                if (session.Complete)
                {
                    backgroundWorkerPainter.ReportProgress(0, "End of session ");
                    break; // если долго ничего не делаем (2 минуты в сумме), то выходим
                }
            }
        }

        private void backgroundWorkerPainter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string s = (string)e.UserState;
            if (s == null) s = "";
            if (!s.Contains("End of session"))
            {
                for (int i = (int)currentFrameNumber; i < (int)session.CurrentFrameNumber; i++)
                {
                    if (session.Frames[i].IsProcessed)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            chart.Series["ReceiveSpeedSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, session.Frames[i].ReceiveSpeed);
                            chart.Series["PacketCountSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, session.Frames[i].PacketCount);
                            chart.Series["ReceiveSpeedMedianSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, session.Frames[i].ReceiveSpeedMedian);
                            if (!session.Frames[i].IsInTime)
                                chart.Series["NotInTimeSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, -0.01);
                            if (!session.Frames[i].IsComplete)
                                chart.Series["IsIncompleteSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, -0.02);
                        }));
                        currentFrameNumber = (ulong)i;
                    }
                }
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    textBoxTest.Text = "[" + textBoxTest.Lines.Count().ToString() + "]" + "Session printing complete" + "\r\n" + textBoxTest.Text;
                    //textBoxTest.ScrollToCaret();
                }));

            }
        }

        private void MonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorkerReader.CancelAsync();
            backgroundWorkerPainter.CancelAsync();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(textBoxSavePath.Text);
            DateTime now = DateTime.Now;
            string dirName = "data " + now.Year.ToString() + "-" + now.Month.ToString() + "-" + now.Day.ToString() + " " + now.Hour.ToString() + "-" + now.Minute.ToString();
            DirectoryInfo subdir = di.CreateSubdirectory(dirName);

            string saveFramesFilename = subdir.ToString() + "\\" + "frames.txt";
            string saveRawDataFilename = subdir.ToString() + "\\" + "rawdata.txt";
            string saveCapturedDataFilename = subdir.ToString() + "\\" + "captureddata.txt";
            session.SaveFrames(saveFramesFilename);

            System.IO.StreamWriter outputfile = new System.IO.StreamWriter(saveRawDataFilename);
            foreach (string s in rawSessionData)
            {
                outputfile.WriteLine(s);
            }
            outputfile.Close();

            File.Copy(textBoxFileName.Text, saveCapturedDataFilename);

            model.SaveEstimate(subdir.ToString());

        }

        //private void backgroundWorkerProcessor_DoWork(object sender, DoWorkEventArgs e)
        //{

        //    while (!backgroundWorkerProcessor.CancellationPending)
        //    {
        //        if (model.ModelSamples.Count < session.Frames.Count)
        //        {
        //            //backgroundWorkerProcessor.ReportProgress(0);
        //            for (int i = model.ModelSamples.Count; i < session.Frames.Count; i++)
        //            {
        //                if (session.Frames[i].IsProcessed)
        //                {
        //                    model.AddFrame(session.Frames[i], model.ModelSamples);
        //                    model.AddFrame(session.Frames[i], model.SystemSamples);
        //                    UnusedForIdentificationFramesCount++;
        //                }
        //                else
        //                {
        //                    //Thread.Sleep(100);
        //                    break;
        //                }
        //            }

        //            if (UnusedForIdentificationFramesCount > ModelReidentificationFramesLimit)
        //            {
        //                UnusedForIdentificationFramesCount = 0;
        //                try
        //                {
        //                    model.TransitionMatrixEstimate();
        //                    model.CDFParamsEstimate();
        //                    model.estimationStartInstance = model.SystemSamples.Max(s => s.Instant);
        //                    canEstimate = true;


        //                }
        //                catch
        //                {
        //                    canEstimate = false;
        //                }
        //            }

        //            //Thread.Sleep(100);
        //            if (canEstimate)
        //            {
        //                model.Process();
        //                model.Estimate();
        //                backgroundWorkerProcessor.ReportProgress(0);
        //                Thread.Sleep(100);
        //            }



        //        }
        //        else
        //        {
        //            if (session.Complete)
        //            {
        //                backgroundWorkerProcessor.ReportProgress(0, "End of session");
        //                break;
        //            }
        //        }
        //        //Thread.Sleep(1000);
        //    }
        //}

        //private void backgroundWorkerProcessor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    string str = (string)e.UserState;
        //    if (str == null) str = "";
        //    if (!str.Contains("End of session"))
        //    {
        //        if (model.estimationStartInstance > 0)
        //        {
        //            this.Invoke(new MethodInvoker(delegate
        //            {
        //                int startIndex = model.EstimateSamples.FindIndex(s => s.Instant > lastShownEstimationInstant && s.XHat != null);
        //                if (startIndex > 0)
        //                {
        //                    textBoxLambda.Text = model.ExportToMatlabString().ToString();
        //                    for (int i = startIndex; i < model.lastEstimatedSample; i++)
        //                    {
        //                        if (model.EstimateSamples[i].XHat != null)
        //                        {
        //                            chartP1.Series["Series1"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[0, 0]);
        //                            chartP2.Series["Series1"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[1, 0]);
        //                            chartP3.Series["Series1"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[2, 0]);
        //                            lastShownEstimationInstant = model.EstimateSamples[i].Instant;
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //            }));
        //        }
        //    }
        //    else
        //    {
        //        this.Invoke(new MethodInvoker(delegate
        //        {
        //            textBoxTest.Text = "[" + textBoxTest.Lines.Count().ToString() + "]" + "Session processing complete" + "\r\n" + textBoxTest.Text;
        //        }));
        //    }
        //}

        private void ModelFill()
        {
            for (int i = 0; i < session.Frames.Count; i++)
            {
                model.AddFrame(session.Frames[i], model.ModelSamples);
                model.AddFrame(session.Frames[i], model.SystemSamples);
            }
        }

        private void buttonIdentify_Click(object sender, EventArgs e)
        {
            ModelFill();
            model.TransitionMatrixEstimate();
            model.CDFParamsEstimate();
            //canEstimate = true;
            textBoxLambda.AppendText("\r\n");
            textBoxLambda.Text = model.ExportToMatlabString().ToString();

            Matrix<double> p = model.p0();
            textBoxLambda.AppendText(p.ToString());
            double d = p[0, 0] * (1 - p[0, 0]) + p[1, 0] * (1 - p[1, 0]) + p[2, 0] * (1 - p[2, 0]);
            textBoxLambda.AppendText("\r\n");
            textBoxLambda.AppendText(d.ToString());

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    model.TransitionMatrixEstimate();
        //    model.CDFParamsEstimate();
        //    //canEstimate = true;
        //    textBoxLambda.Text = model.ExportToMatlabString().ToString();
        //}

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            model.Init(double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text), int.Parse(textBoxPC_b.Text));
            ModelFill();
            model.Process();
            //model.Estimate();
            int packSize = 10;
            int currentPackSize = 0;
            for (int i = 0; i < model.EstimateSamples.Count - 1; i++)
            {
                if (model.EstimateNext(i))
                {
                    if (currentPackSize == packSize)
                    {
                        for (int k = i - packSize; k <= i; k++)
                        {
                            chart.Series["E1Series"].Points.AddXY(model.EstimateSamples[k].Instant, model.EstimateSamples[k].XHat[0, 0] - 5);
                            chart.Series["E2Series"].Points.AddXY(model.EstimateSamples[k].Instant, model.EstimateSamples[k].XHat[1, 0] - 3);
                            chart.Series["E3Series"].Points.AddXY(model.EstimateSamples[k].Instant, model.EstimateSamples[k].XHat[2, 0] - 1);
                        }
                        currentPackSize = 0;
                    }
                    currentPackSize++;
                }
                else
                {
                    break;
                }
                //chart.Series["E1Series"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[0, 0] - 5);
                //chart.Series["E2Series"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[1, 0] - 3);
                //chart.Series["E3Series"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[2, 0] - 1);
            }
            //foreach (Sample s in model.EstimateSamples)
            //{
            //    if (s.XHat != null)
            //    {
            //        chart.Series["E1Series"].Points.AddXY(s.Instant, s.XHat[0, 0] - 5);
            //        chart.Series["E2Series"].Points.AddXY(s.Instant, s.XHat[1, 0] - 3);
            //        chart.Series["E3Series"].Points.AddXY(s.Instant, s.XHat[2, 0] - 1);
            //    }
            //}
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {

            Init();
            model.Init(double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text), int.Parse(textBoxPC_b.Text));
            string fileName = textBoxFileName.Text;
            using (FileStream fs = new FileStream
               (fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        if (checkBoxTSharkRTPData.Checked)
                        {
                            ProcessTSharkString(s);
                        }
                        else
                        {
                            ProcessLegacyString(s);
                        }
                    }
                }
            }


            for (int i = 0; i < (int)session.CurrentFrameNumber; i++)
            {
                chart.Series["ReceiveSpeedSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, session.Frames[i].ReceiveSpeed);
                chart.Series["PacketCountSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, session.Frames[i].PacketCount);
                //chart.Series["SizeSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, (double)session.Frames[i].Size / 1000.0 - 10);
                chart.Series["ReceiveSpeedMedianSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, session.Frames[i].ReceiveSpeedMedian);
                if (!session.Frames[i].IsInTime)
                    chart.Series["NotInTimeSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, -0.01);
                if (!session.Frames[i].IsComplete)
                    chart.Series["IsIncompleteSeries"].Points.AddXY(session.Frames[i].LastPacketReceptionTime, -0.02);
            }
        }

        private void textBoxTest_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Init();
            model.Init(double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text), int.Parse(textBoxPC_b.Text));
            for (int i = 0; i < chart.Series.Count; i++)
            {
                chart.Series[i].Points.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //double a = double.Parse(textBoxRSM_lb.Text.ToString());
            //double b = double.Parse(textBoxRSM_ub.Text.ToString());
            //double t = double.Parse(PC_b.Text.ToString());

            //textBoxTest.Text = RTPModel.GammaCDF(t, new GammaParams(a, b)).ToString();

            double time_min = session.Frames[0].LastPacketReceptionTime;
            double time_max = session.Frames[session.Frames.Count - 1].LastPacketReceptionTime;

            chart.Series["RSMBoundsSeries"].Points.AddXY(time_min, double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text));
            chart.Series["RSMBoundsSeries"].Points.AddXY(time_max, double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text));

            chart.Series["PCBoundSeries"].Points.AddXY(time_min, double.Parse(textBoxPC_b.Text));
            chart.Series["PCBoundSeries"].Points.AddXY(time_max, double.Parse(textBoxPC_b.Text));
            //model.Init(double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_lb.Text), int.Parse(textBoxRSM_lb.Text));


        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart.Series["RSMBoundsSeries"].Points.Clear();
            chart.Series["PCBoundSeries"].Points.Clear();
        }

        private void buttonParamsSave_Click(object sender, EventArgs e)
        {
            string fileName = textBoxFileName.Text;
            FileInfo file = new FileInfo(fileName);
            //DirectoryInfo dir = new DirectoryInfo(fileName);
            model.ExportGammaData(file.DirectoryName);
            model.ExportTimesInStateData(file.DirectoryName);
        }

        private void buttonIdentifyProcess_Click(object sender, EventArgs e)
        {
            model.Init(double.Parse(textBoxRSM_lb.Text), double.Parse(textBoxRSM_ub.Text), int.Parse(textBoxPC_b.Text));

            for (int i = 0; i < session.Frames.Count; i++)
            {
                //model.AddFrame(session.Frames[i], model.ModelSamples);
                model.AddFrame(session.Frames[i], model.SystemSamples);
            }

            model.Process();


            string fileName = textBoxFileName.Text;
            FileInfo file = new FileInfo(fileName);

            //DirectoryInfo dir = new DirectoryInfo(fileName);
            //;


            int IdentifyLag = (int)session.WindowSize;
            int IdentifyGap = 1;
            int IdentifyPool = 0;
            bool canEstimate = true;

            for (int i = 0; i < model.EstimateSamples.Count - 1; i++)
            {
                if (model.EstimateSamples[i].Frame != null)
                {
                    IdentifyPool++;
                    model.AddFrame(model.EstimateSamples[i].Frame, model.ModelSamples);
                    if (IdentifyPool == IdentifyGap)
                    {
                        model.TransitionMatrixEstimate();
                        model.CDFParamsEstimate();
                        textBoxLambda.Text = model.ExportToMatlabString().ToString();
                        IdentifyPool = 0;
                        canEstimate = true;

                        //model.ExportGammaParams(file.DirectoryName, model.EstimateSamples[i].Frame.LastPacketReceptionTime);

                    }
                }

                if (canEstimate && i + IdentifyLag < model.EstimateSamples.Count - 1)
                {
                    if (model.EstimateNext(i+IdentifyLag))
                    {
                        canEstimate = true;
                    }
                    else
                    {
                        canEstimate = false;
                        //break;
                    }
                }
            }

            for (int i = 0; i < model.EstimateSamples.Count - 1; i++)
            {
                if (model.EstimateSamples[i].XHat != null)
                {
                    chart.Series["E1Series"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[0, 0] - 5);
                    chart.Series["E2Series"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[1, 0] - 3);
                    chart.Series["E3Series"].Points.AddXY(model.EstimateSamples[i].Instant, model.EstimateSamples[i].XHat[2, 0] - 1);
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBoxRSM_lb.Text = Exponential.PDF(1/double.Parse(textBoxPC_b.Text),5).ToString();
            textBoxRSM_ub.Text = Exponential.CDF(1/double.Parse(textBoxPC_b.Text),5).ToString();
        }
    }
}
