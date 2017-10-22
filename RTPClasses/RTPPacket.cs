using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RTPClasses
{
    [Serializable]
    public class RTPPacket
    {
        public UInt64 Number;
        public UInt64 ReceptionNumber;
        public UInt64 TimeStampSender;
        public UInt64 TimeStampReceiver;
        public double OffsetSender;
        public double OffsetReceiver;
        public bool Marked;
        public bool Corrupted = false;
        public bool? InOrder = null;
        public UInt64 Size = 0;

        public RTPPacket()
        { 
        }

        public RTPPacket(string[] row)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            try
            {
                Number = Convert.ToUInt64(row[5]);
                ReceptionNumber = Convert.ToUInt64(row[0]);
                TimeStampSender = Convert.ToUInt64(row[4]);
                TimeStampReceiver = Convert.ToUInt64(row[3]);
                OffsetSender = Convert.ToDouble(row[2], provider);
                OffsetReceiver = Convert.ToDouble(row[1], provider);
                Marked = Convert.ToBoolean(Convert.ToByte(row[6]));
            }
            catch
            {
                Corrupted = true;
            }
        }


        public static RTPPacket ImportTShark(string[] row)
        {
            RTPPacket result = new RTPPacket();
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            try
            {
                result.Number = Convert.ToUInt64(row[9].TrimEnd(',').Remove(0,4)); //Seq=123,
                result.ReceptionNumber = Convert.ToUInt64(row[0]);
                result.TimeStampSender = Convert.ToUInt64(row[10].TrimEnd(',').Remove(0,5)); //Time=123,
                result.TimeStampReceiver = 0;
                result.OffsetSender = 0;
                result.OffsetReceiver = Convert.ToDouble(row[1], provider);
                result.Marked = false;
                if (row.Length == 12)
                {
                    if (row[11] == "Mark")
                        result.Marked = true;
                }
                result.Size = Convert.ToUInt64(row[6]); ;
            }
            catch
            {
                result.Corrupted = true;
            }
            return result;

        }
        override public string ToString()
        {
            string result = string.Format("{0} ({1}) sent: {2} ({4}s) received: {3} ({5}s marked={6} inorder={8} corrupted={7}), size={8}", Number, ReceptionNumber, TimeStampSender, TimeStampReceiver, OffsetSender, OffsetReceiver, Marked, Corrupted, InOrder, Size);
            return result;
        }
    }
}
