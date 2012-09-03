using System;
using NLog;

namespace PresentationGenerator.Core.Utility
{
    public interface ILogEventContext
    {
        void Set(LogEventInfo eventInfo);
    }
}