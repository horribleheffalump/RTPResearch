using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RTPClasses
{
    [Serializable]
    public class RTPSession
    {
        public List<RTPPacket> Packets;
        public List<RTPFrame> Frames;
        //public List<RTPFrame> TempFrames;
        public UInt64 CurrentPacketNumber;
        public UInt64 CurrentFrameNumber;
        //public UInt64 TempCurrentFrameNumber;
        //public double PlayoutIncrement = 0;
        //public double MaxOffsetReceiver = 0;
        //public int InitialPlayOutOffsetMs;
        //public double InitialPlayOutOffsetS;
        public ulong WindowSize = 5;
        public bool Complete = false;
        //public List<double> ReceiveSpeedStateLimits;

        public RTPSession()
        {

            Packets = new List<RTPPacket>();
            CurrentPacketNumber = 0;

            Frames = new List<RTPFrame>();
            CurrentFrameNumber = 0;


            //TempFrames = new List<RTPFrame>();
            //TempCurrentFrameNumber = 0;

            //InitialPlayOutOffsetMs = 300;
            //InitialPlayOutOffsetS = 0.001 * InitialPlayOutOffsetMs;

            //ReceiveSpeedStateLimits = new List<double>();
            //ReceiveSpeedStateLimits.Add(0.015);
            //ReceiveSpeedStateLimits.Add(0.04);
        }

        public bool Add(RTPPacket p)
        {
            bool result = false;
            if (p.Number >= CurrentPacketNumber)
            {
                CurrentPacketNumber = p.Number;
                if (CurrentPacketNumber == 65535)
                    CurrentPacketNumber = 0;
                p.InOrder = true;
            }
            else
            {
                p.InOrder = false;
            }


            Packets.Add(p);

            if (CurrentFrameNumber == 0) // вставляем первый фрейм
            {
                Frames.Add(new RTPFrame(CurrentFrameNumber, p.TimeStampSender));
                CurrentFrameNumber++;
                Frames[(int)CurrentFrameNumber - 1].AddPacket(p);
            }
            else
            {
                for (int i = 1; i < 5; i++)
                {
                    if (p.TimeStampSender == Frames[Math.Max(0, (int)CurrentFrameNumber - i)].TimeStampSender)
                    {
                        Frames[(int)CurrentFrameNumber - i].AddPacket(p);
                       
                        break;
                    }
                    else if (i == 4)
                    {
                        Frames.Add(new RTPFrame(CurrentFrameNumber, p.TimeStampSender));
                        CurrentFrameNumber++;
                        Frames[(int)CurrentFrameNumber - 1].AddPacket(p);
                    }

                }
            }

            SetFrameReceptionDurations(Frames[(int)CurrentFrameNumber - 1]);
            if (CurrentFrameNumber > 2 * WindowSize)
            {
                if (CurrentFrameNumber == 2 * WindowSize+1)
                {
                    for (int i = 0; (ulong)i <= WindowSize; i++)
                    {
                        if (!Frames[i].IsProcessed) 
                        {
                            ProcessFrame(Frames[i]);
                        }
                    }
                }
                else
                {
                    if (!Frames[(int)(CurrentFrameNumber - WindowSize - 1)].IsProcessed) 
                    {
                        ProcessFrame(Frames[(int)(CurrentFrameNumber - WindowSize - 1)]);
                    }
                }
            }

            //if (Frames.Count(s => s.TimeStampSender == p.TimeStampSender) == 0)
            //{
            //    Frames.Add(new RTPFrame(CurrentFrameNumber, p.TimeStampSender));
            //    CurrentFrameNumber++;
            //}


            result = true;
            return result;
        }

        //public void AssignPlayTime()
        //{
        //    MaxOffsetReceiver = Packets.Last().OffsetReceiver;
        //    PlayoutIncrement = MaxOffsetReceiver / CurrentFrameNumber;
        //    foreach (RTPFrame frame in Frames)
        //    {
        //        frame.PlayTime = InitialPlayOutOffsetS + frame.Number * PlayoutIncrement;
        //    }
        //}

        public void ProcessFrame(RTPFrame f)
        {
            //foreach (RTPFrame f in Frames)
            //{
            //    SetLastPacketReceptionTimes(f);
            //}
            //foreach (RTPFrame f in Frames)
            //{
            //    SetMarkedPacketReceived(f);
            //}
            //foreach (RTPFrame f in Frames)
            //{
            //    SetPacketCount(f);
            //}
            //foreach (RTPFrame f in Frames)
            //{
            //    SetAreThereOutOfOrder(f);
            //}

            SetIsTheFrameInTime(f, 1);
            SetIsTheFrameComplete(f);
            //SetFrameReceptionDurations(f); // must be done befrore all
            SetRecieveSpeedMedian(f, WindowSize);
            SetPacketCountMedian(f, WindowSize);
            SetPacketCountAverage(f, WindowSize);
            f.IsProcessed = true;
        }

        public void SetLastPacketReceptionTimes(RTPFrame frame)
        {
            frame.LastPacketReceptionTime = Packets.Where(s => s.TimeStampSender == frame.TimeStampSender).Max(s => s.OffsetReceiver);
        }

        public void SetFrameReceptionDurations(RTPFrame frame)
        {
            double LastFrameReceptionTime = 0;
            if (frame.Number > 0)
            {
                LastFrameReceptionTime = Frames.Find(s => s.Number == frame.Number - 1).LastPacketReceptionTime;
            }
            else
            {
                LastFrameReceptionTime = Packets.Min(s => s.OffsetReceiver);
            }
            frame.ReceptionDuration = frame.LastPacketReceptionTime - LastFrameReceptionTime;
            frame.ReceiveSpeed = frame.ReceptionDuration / frame.PacketCount;
        }

        public void SetRecieveSpeedMedian(RTPFrame frame, ulong windowSize)
        {
            //List<double> samples;
            //List<double> samplesSorted = new List<double>();
            int lowBound = (int)(frame.Number - windowSize);
            int upBound = (int)(frame.Number + windowSize);

            var samples = Frames.Where(s => ((int)s.Number > lowBound) && ((int)s.Number < upBound)).Select(s => s.ReceiveSpeed).OrderBy(s => s).ToList();
            // && (s.Number < frame.Number + windowSize)
            //samples.Add(frame.ReceiveSpeed);
            //if (samples.Count == windowSize + 1)
            //{
            //    samples.RemoveAt(0);
            //}

            //samplesSorted = samples.OrderBy(s => s).ToList();
            double mid = (samples.Count - 1) / 2.0;
            frame.ReceiveSpeedMedian = samples[(int)mid];
            //return (ys[(int)(mid)] + ys[(int)(mid + 0.5)]) / 2;

        }

        public void SetPacketCountMedian(RTPFrame frame, ulong windowSize)
        {
            int lowBound = (int)(frame.Number - windowSize);
            int upBound = (int)(frame.Number + windowSize);

            var samples = Frames.Where(s => ((int)s.Number > lowBound) && ((int)s.Number < upBound)).Select(s => s.PacketCount).OrderBy(s => s).ToList();
            double mid = (samples.Count - 1) / 2.0;
            frame.PacketCountMedian = samples[(int)mid];
        }

        public void SetPacketCountAverage(RTPFrame frame, ulong windowSize)
        {
            int lowBound = (int)(frame.Number - windowSize);
            int upBound = (int)(frame.Number + windowSize);

            frame.PacketCountAverage = Frames.Where(s => ((int)s.Number > lowBound) && ((int)s.Number < upBound)).Select(s => s.PacketCount).Average(s => s);
        }


        public void SetMarkedPacketReceived(RTPFrame frame)
        {
            frame.MarkedPacketReceived = Packets.Exists(s => s.TimeStampSender == frame.TimeStampSender && s.Marked);
        }

        public void SetPacketCount(RTPFrame frame)
        {
            frame.PacketCount = Packets.Count(s => s.TimeStampSender == frame.TimeStampSender);
        }

        public void SetAreThereOutOfOrder(RTPFrame frame)
        {
            frame.AreThereOutOfOrder = Packets.Exists(s => s.TimeStampSender == frame.TimeStampSender && !s.InOrder.Value);
        }

        public void SetIsTheFrameInTime(RTPFrame frame, ulong NumberOfFramesToWait)
        {
            if (Frames.Exists(s => s.Number == frame.Number + NumberOfFramesToWait))
            {
                double UpcomingPacketReceptionTime = Frames.Where(s => s.Number == frame.Number + NumberOfFramesToWait).First().LastPacketReceptionTime;
                frame.IsInTime = (frame.LastPacketReceptionTime < UpcomingPacketReceptionTime);
            }
        }

        public void SetIsTheFrameComplete(RTPFrame frame)
        {
            frame.IsComplete = false;
            if (frame.MarkedPacketReceived)
            {
                UInt64 IntendedFirst = 0; // какой должен быть номер первого пакета во фрейме
                UInt64 MarkedPacketNumber = Packets.Find(s => s.TimeStampSender == frame.TimeStampSender && s.Marked).Number; //номер пакета с маркой                    
                if (Frames.Exists(s => s.Number == frame.Number - 1)) // есть предыдущий фрейм, считаем, что первым пакетом должен быть либо следующий за маркированным, либо через один от последнего в предыдущем фрейме
                {
                    RTPFrame PreviousFrame = Frames.Find(s => s.Number == frame.Number - 1);
                    if (Packets.Exists(s => s.TimeStampSender == PreviousFrame.TimeStampSender && s.Marked))
                    {
                        RTPPacket PreviousFrameMarkedPacket = Packets.Find(s => s.TimeStampSender == PreviousFrame.TimeStampSender && s.Marked);
                        IntendedFirst = PreviousFrameMarkedPacket.Number + 1;
                    }
                    else
                        IntendedFirst = Packets.Where(s => s.TimeStampSender == PreviousFrame.TimeStampSender).Max(s => s.Number) + 2;
                }
                else // предыдущего фрейма нет, считаем, что первый - это первый в текущем фрейме
                {
                    IntendedFirst = Packets.Where(s => s.TimeStampSender == frame.TimeStampSender).Min(s => s.Number);
                }

                frame.IsComplete = (frame.PacketCount == (int)(MarkedPacketNumber - IntendedFirst + 1)); // фрейм полный, если количество пакетов в нем равно разности между номером маркированного пакета и предполагаемого первого пакета + 1
            }
        }

        public static RTPSession Load(string file)
        {
            RTPSession s = new RTPSession();
            XmlSerializer formatter = new XmlSerializer(s.GetType());
            FileStream aFile = new FileStream(file, FileMode.Open);
            byte[] buffer = new byte[aFile.Length];
            aFile.Read(buffer, 0, (int)aFile.Length);
            MemoryStream stream = new MemoryStream(buffer);
            aFile.Close();
            return (RTPSession)formatter.Deserialize(stream);
        }

        public static RTPSession LoadPackets(string file)
        {
            RTPSession s = new RTPSession();
            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(' ');

                s.Add(new RTPPacket(values));
            }
            reader.Close();
            return s;
        }


        public void Save(string path)
        {
            FileStream outFile = File.Create(path);
            XmlSerializer formatter = new XmlSerializer(this.GetType());
            formatter.Serialize(outFile, this);
            outFile.Close();
        }

        public void SaveFrames(string path)
        {
            System.IO.StreamWriter outputfile = new System.IO.StreamWriter(path);
            foreach (RTPFrame frame in Frames.OrderBy(s => s.Number))
            {
                outputfile.WriteLine(frame.Export());
            }
            outputfile.Close();
        }

    }
}
