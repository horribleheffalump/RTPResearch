namespace RTPMonitor
{
    partial class MonitorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.backgroundWorkerReader = new System.ComponentModel.BackgroundWorker();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.backgroundWorkerPainter = new System.ComponentModel.BackgroundWorker();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoxTSharkRTPData = new System.Windows.Forms.CheckBox();
            this.textBoxLambda = new System.Windows.Forms.TextBox();
            this.buttonIdentify = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.textBoxTest = new System.Windows.Forms.TextBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDrawBounds = new System.Windows.Forms.Button();
            this.textBoxRSM_lb = new System.Windows.Forms.TextBox();
            this.textBoxRSM_ub = new System.Windows.Forms.TextBox();
            this.textBoxPC_b = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonParamsSave = new System.Windows.Forms.Button();
            this.buttonIdentifyProcess = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart.Legends.Add(legend2);
            this.chart.Location = new System.Drawing.Point(277, 0);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series12.Legend = "Legend1";
            series12.Name = "RSMBoundsSeries";
            series12.YValuesPerPoint = 2;
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Legend = "Legend1";
            series13.Name = "ReceiveSpeedSeries";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Legend = "Legend1";
            series14.Name = "ReceiveSpeedMedianSeries";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series15.Legend = "Legend1";
            series15.Name = "NotInTimeSeries";
            series15.YValuesPerPoint = 2;
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series16.Legend = "Legend1";
            series16.Name = "IsIncompleteSeries";
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series17.Legend = "Legend1";
            series17.Name = "PacketCountSeries";
            series17.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series18.ChartArea = "ChartArea1";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series18.Legend = "Legend1";
            series18.Name = "E1Series";
            series18.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series19.ChartArea = "ChartArea1";
            series19.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series19.Legend = "Legend1";
            series19.Name = "E2Series";
            series19.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series20.ChartArea = "ChartArea1";
            series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series20.Legend = "Legend1";
            series20.Name = "E3Series";
            series20.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series21.ChartArea = "ChartArea1";
            series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series21.Legend = "Legend1";
            series21.Name = "PCBoundSeries";
            series21.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series22.ChartArea = "ChartArea1";
            series22.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series22.Legend = "Legend1";
            series22.Name = "SizeSeries";
            series22.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chart.Series.Add(series12);
            this.chart.Series.Add(series13);
            this.chart.Series.Add(series14);
            this.chart.Series.Add(series15);
            this.chart.Series.Add(series16);
            this.chart.Series.Add(series17);
            this.chart.Series.Add(series18);
            this.chart.Series.Add(series19);
            this.chart.Series.Add(series20);
            this.chart.Series.Add(series21);
            this.chart.Series.Add(series22);
            this.chart.Size = new System.Drawing.Size(773, 846);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(259, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "tshark -d udp.port==0-9999,rtp -f \"host 10.4.44.105\"";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(115, 170);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(13, 65);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(259, 20);
            this.textBoxFileName.TabIndex = 3;
            this.textBoxFileName.Text = "..\\RTPResearch\\CAPTools\\Data\\data 2015-12-7 21-29 - 3g 20min\\rawda" +
    "ta.txt";
            // 
            // backgroundWorkerReader
            // 
            this.backgroundWorkerReader.WorkerReportsProgress = true;
            this.backgroundWorkerReader.WorkerSupportsCancellation = true;
            this.backgroundWorkerReader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReader_DoWork);
            this.backgroundWorkerReader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerReader_ProgressChanged);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(196, 170);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(12, 39);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(259, 20);
            this.textBoxFilter.TabIndex = 6;
            this.textBoxFilter.Text = "-> 10.4.44.105  RTP";
            // 
            // backgroundWorkerPainter
            // 
            this.backgroundWorkerPainter.WorkerReportsProgress = true;
            this.backgroundWorkerPainter.WorkerSupportsCancellation = true;
            this.backgroundWorkerPainter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPainter_DoWork);
            this.backgroundWorkerPainter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerPainter_ProgressChanged);
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(12, 199);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(259, 20);
            this.textBoxSavePath.TabIndex = 7;
            this.textBoxSavePath.Text = "..\\..\\..\\Data\\";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(195, 226);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoxTSharkRTPData
            // 
            this.checkBoxTSharkRTPData.AutoSize = true;
            this.checkBoxTSharkRTPData.Checked = true;
            this.checkBoxTSharkRTPData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTSharkRTPData.Location = new System.Drawing.Point(13, 92);
            this.checkBoxTSharkRTPData.Name = "checkBoxTSharkRTPData";
            this.checkBoxTSharkRTPData.Size = new System.Drawing.Size(110, 17);
            this.checkBoxTSharkRTPData.TabIndex = 9;
            this.checkBoxTSharkRTPData.Text = "TShark RTP data";
            this.checkBoxTSharkRTPData.UseVisualStyleBackColor = true;
            // 
            // textBoxLambda
            // 
            this.textBoxLambda.Location = new System.Drawing.Point(12, 279);
            this.textBoxLambda.Multiline = true;
            this.textBoxLambda.Name = "textBoxLambda";
            this.textBoxLambda.Size = new System.Drawing.Size(258, 142);
            this.textBoxLambda.TabIndex = 10;
            // 
            // buttonIdentify
            // 
            this.buttonIdentify.Location = new System.Drawing.Point(194, 428);
            this.buttonIdentify.Name = "buttonIdentify";
            this.buttonIdentify.Size = new System.Drawing.Size(75, 23);
            this.buttonIdentify.TabIndex = 11;
            this.buttonIdentify.Text = "Identify";
            this.buttonIdentify.UseVisualStyleBackColor = true;
            this.buttonIdentify.Click += new System.EventHandler(this.buttonIdentify_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(194, 457);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonProcess.TabIndex = 16;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // textBoxTest
            // 
            this.textBoxTest.Location = new System.Drawing.Point(14, 515);
            this.textBoxTest.Multiline = true;
            this.textBoxTest.Name = "textBoxTest";
            this.textBoxTest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTest.Size = new System.Drawing.Size(255, 113);
            this.textBoxTest.TabIndex = 17;
            this.textBoxTest.TextChanged += new System.EventHandler(this.textBoxTest_TextChanged);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(196, 141);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 19;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(115, 141);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 20;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonDrawBounds
            // 
            this.buttonDrawBounds.Location = new System.Drawing.Point(193, 635);
            this.buttonDrawBounds.Name = "buttonDrawBounds";
            this.buttonDrawBounds.Size = new System.Drawing.Size(75, 23);
            this.buttonDrawBounds.TabIndex = 21;
            this.buttonDrawBounds.Text = "Bounds";
            this.buttonDrawBounds.UseVisualStyleBackColor = true;
            this.buttonDrawBounds.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxRSM_lb
            // 
            this.textBoxRSM_lb.Location = new System.Drawing.Point(14, 634);
            this.textBoxRSM_lb.Name = "textBoxRSM_lb";
            this.textBoxRSM_lb.Size = new System.Drawing.Size(100, 20);
            this.textBoxRSM_lb.TabIndex = 22;
            this.textBoxRSM_lb.Text = "0,04";
            // 
            // textBoxRSM_ub
            // 
            this.textBoxRSM_ub.Location = new System.Drawing.Point(14, 661);
            this.textBoxRSM_ub.Name = "textBoxRSM_ub";
            this.textBoxRSM_ub.Size = new System.Drawing.Size(100, 20);
            this.textBoxRSM_ub.TabIndex = 23;
            this.textBoxRSM_ub.Text = "0,08";
            // 
            // textBoxPC_b
            // 
            this.textBoxPC_b.Location = new System.Drawing.Point(14, 687);
            this.textBoxPC_b.Name = "textBoxPC_b";
            this.textBoxPC_b.Size = new System.Drawing.Size(100, 20);
            this.textBoxPC_b.TabIndex = 24;
            this.textBoxPC_b.Text = "6";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 665);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonParamsSave
            // 
            this.buttonParamsSave.Location = new System.Drawing.Point(115, 428);
            this.buttonParamsSave.Name = "buttonParamsSave";
            this.buttonParamsSave.Size = new System.Drawing.Size(75, 23);
            this.buttonParamsSave.TabIndex = 26;
            this.buttonParamsSave.Text = "Param save";
            this.buttonParamsSave.UseVisualStyleBackColor = true;
            this.buttonParamsSave.Click += new System.EventHandler(this.buttonParamsSave_Click);
            // 
            // buttonIdentifyProcess
            // 
            this.buttonIdentifyProcess.Location = new System.Drawing.Point(196, 487);
            this.buttonIdentifyProcess.Name = "buttonIdentifyProcess";
            this.buttonIdentifyProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonIdentifyProcess.TabIndex = 27;
            this.buttonIdentifyProcess.Text = "IdentifyProcess";
            this.buttonIdentifyProcess.UseVisualStyleBackColor = true;
            this.buttonIdentifyProcess.Click += new System.EventHandler(this.buttonIdentifyProcess_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(196, 695);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 28;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 845);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonIdentifyProcess);
            this.Controls.Add(this.buttonParamsSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxPC_b);
            this.Controls.Add(this.textBoxRSM_ub);
            this.Controls.Add(this.textBoxRSM_lb);
            this.Controls.Add(this.buttonDrawBounds);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.textBoxTest);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.buttonIdentify);
            this.Controls.Add(this.textBoxLambda);
            this.Controls.Add(this.checkBoxTSharkRTPData);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chart);
            this.Name = "MonitorForm";
            this.Text = "MonitorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonitorForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReader;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPainter;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxTSharkRTPData;
        private System.Windows.Forms.TextBox textBoxLambda;
        private System.Windows.Forms.Button buttonIdentify;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.TextBox textBoxTest;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonDrawBounds;
        private System.Windows.Forms.TextBox textBoxRSM_lb;
        private System.Windows.Forms.TextBox textBoxRSM_ub;
        private System.Windows.Forms.TextBox textBoxPC_b;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonParamsSave;
        private System.Windows.Forms.Button buttonIdentifyProcess;
        private System.Windows.Forms.Button button2;
    }
}

