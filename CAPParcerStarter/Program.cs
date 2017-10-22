using RTPClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPParcerStarter
{
    class Program
    {
        static string CapParcerExeFile = "CAPTest.exe";

        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            Dictionary<string, TimeSpan> durations = new Dictionary<string, TimeSpan>();
            //TimeSpan capProcessTime = new TimeSpan(0);
            //TimeSpan framesProcessTime = new TimeSpan(0);
            //TimeSpan fullProcessTime = new TimeSpan(0);

            List<string> fileNames = new List<string>();
            fileNames.Add(args[0]);
            fileNames.Add(args[1]);
            List<RTPSession> sessions = new List<RTPSession>();

            foreach (string fileName in fileNames)
            {
                string folderName = Path.GetDirectoryName(fileName);
                string file = Path.GetFileNameWithoutExtension(fileName);
                folderName = Path.Combine(folderName, file);

                bool txtExists = false;
                if (Directory.Exists(folderName))
                {
                    DirectoryInfo dir = new DirectoryInfo(folderName);
                    if (dir.EnumerateFiles("*_V.txt").Count() > 0)
                    {
                        txtExists = true;
                    }
                }

                DateTime processCapStartTime = DateTime.Now;
                if (!txtExists)
                {
                    ProcessCapFile(CapParcerExeFile, fileName);
                }
                durations.Add(fileName + " cap processed in ", processCapStartTime.Subtract(DateTime.Now));

                DirectoryInfo di = new DirectoryInfo(folderName);
                FileInfo fi = di.EnumerateFiles("*_V.txt").OrderByDescending(s => s.Length).First();
                string basefileName = Path.GetFileNameWithoutExtension(fi.FullName);
                string savefileName = folderName + "\\" + basefileName + "_frames_serialized.xml";
                string outputfileName = folderName + "\\" + basefileName + "_frames.txt";
                string outputMfileName = folderName + "\\" + basefileName + "_model.m";

                DateTime processFramesTime = DateTime.Now;
                RTPSession session = new RTPSession();
                if (!File.Exists(savefileName) || !File.Exists(outputfileName))
                {
                    session = RTPSession.LoadPackets(fi.FullName);
                    //session.ProcessFrames();
                    session.Save(savefileName);
                    session.SaveFrames(outputfileName);
                }
                else
                {
                    session = RTPSession.Load(savefileName);
                }
                durations.Add(fileName + " frames processed in ", processCapStartTime.Subtract(DateTime.Now));

                sessions.Add(session);
            }
            DateTime processModelStartTime = DateTime.Now;
            string folderOut = Path.GetDirectoryName(args[1]);
            string fileOut = Path.GetFileNameWithoutExtension(args[1]);
            folderOut = Path.Combine(folderOut, fileOut);

            //RTPModel model = new RTPModel(sessions[0], sessions[1], folderOut); // передалал всё нафиг, теперь это работает не так

            durations.Add("Model processed in ", processModelStartTime.Subtract(DateTime.Now));
            durations.Add("The whole thing processed in ", startTime.Subtract(DateTime.Now));

            //fullProcessTime = startTime.Subtract(DateTime.Now);
            //model.SaveToMatlab(outputMfileName);

            SaveToStats(Path.GetDirectoryName(args[1]) + "\\stats.txt", durations);


        }

        static void ProcessCapFile(string exefilename, string capfilename)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.FileName = exefilename;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "\"" + capfilename + "\" 1 0";

            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        static void SaveToStats(string path, Dictionary<string, TimeSpan> durations)
        {
            System.IO.StreamWriter outputStatsfile = new System.IO.StreamWriter(path);
            StringBuilder contents = new StringBuilder();
            foreach (KeyValuePair<string, TimeSpan> pair in durations)
            {
                contents.AppendLine(pair.Key + "\t" + pair.Value.Duration().ToString());
            }


            outputStatsfile.Write(contents.ToString());
            outputStatsfile.Close();
        }

    }

}
