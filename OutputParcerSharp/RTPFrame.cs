using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTPClasses
{
    [Serializable]
    public class RTPFrame
    {
        public UInt64 Number;
        public UInt64 TimeStampSender;
        public double LastPacketReceptionTime = 0;
        public double ReceptionDuration = 0;
        public bool MarkedPacketReceived = false;
        public int PacketCount = 0;
        public int PacketCountMedian = 0;
        public double PacketCountAverage = 0;
        public double PlayTime = 0;
        public bool AreThereOutOfOrder = false;
        public bool IsComplete = false;
        public bool IsInTime = true;
        public double ReceiveSpeed = 0;
        public double ReceiveSpeedMedian = 0;
        //public int State;
        public bool IsProcessed = false;
        public UInt64 Size = 0;

        public List<RTPPacket> Packets;

        public RTPFrame()
        {
            Packets = new List<RTPPacket>();
        }

        public RTPFrame(UInt64 number, UInt64 timeStampSender)
        {
            Packets = new List<RTPPacket>();
            TimeStampSender = timeStampSender;
            Number = number;
        }


        override public string ToString()
        {
            string result = string.Format("{0} timestamp: {1} count: {3} marked: {4}  lastreceived: {5} playout: {2} outoforder: {6} IsComplete: {7} Size: {8}", Number, TimeStampSender, PlayTime, PacketCount, MarkedPacketReceived, LastPacketReceptionTime, AreThereOutOfOrder, IsComplete, Size);
            return result;
        }

        public string Export()
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            string result = string.Format(provider, "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}", Number, PacketCount, PacketCountMedian, PacketCountAverage, TimeStampSender, LastPacketReceptionTime, ReceptionDuration, Convert.ToInt16(IsComplete), Convert.ToInt16(MarkedPacketReceived), Convert.ToInt16(AreThereOutOfOrder), ReceiveSpeed, ReceiveSpeedMedian, Convert.ToInt16(IsInTime), Size);
            return result;
        }

        public void AddPacket(RTPPacket p)
        {
            Packets.Add(p);
            PacketCount = Packets.Count;
            if (LastPacketReceptionTime < p.OffsetReceiver) LastPacketReceptionTime = p.OffsetReceiver;
            if (p.Marked) MarkedPacketReceived = true;
            if (!p.InOrder.Value) AreThereOutOfOrder = true;
            Size += p.Size;
        }
    }

}
