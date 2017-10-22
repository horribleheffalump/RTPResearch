using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTPClasses
{
    public class RTPState
    {
        public int StateNumber;
        public int Priority;
        public string StateName;
        public string StateDescription;
        public Func<RTPFrame, bool> Condition;


        public RTPState(int stateNumber, int priority, string stateName, string stateDescription, Func<RTPFrame, bool> condition)
        {
            StateNumber = stateNumber;
            Priority = priority;
            StateName = stateName;
            StateDescription = stateDescription;
            Condition = condition;
        }
    }

    public class RTPObservation
    { 
        public int ObservationNumber;
        public int Priority;
        public string ObservationName;
        public string ObservationDescription;
        public Func<RTPFrame, bool> Condition;


        public RTPObservation(int obsNumber, int priority, string obsName, string obsDescription, Func<RTPFrame, bool> condition)
        {
            ObservationNumber = obsNumber;
            Priority = priority;
            ObservationName = obsName;
            ObservationDescription = obsDescription;
            Condition = condition;
        }
    }
}
