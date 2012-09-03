using NLog;

namespace PresentationGenerator.Core.Utility
{
    public class LogEventContext : ILogEventContext
    {
        private LogEventInfo _eventInfo;
        public void Set(LogEventInfo eventInfo)
        {
            eventInfo = _eventInfo;
        }
    }
}