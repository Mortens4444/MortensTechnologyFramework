using System;

namespace Mtf.Hardware.Raid.Areca
{
    public class ArecaEvent
    {
        public DateTime EventTime;
        public string Device;
        public string EventMessage;

        public ArecaEvent(DateTime eventTime, string device, string eventMessage)
        {
            EventTime = eventTime;
            Device = device;
            EventMessage = eventMessage;
        }
    }
}