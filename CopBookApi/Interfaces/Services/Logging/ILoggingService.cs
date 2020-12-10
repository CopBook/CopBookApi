using Microsoft.Extensions.Logging;

namespace CopBookApi.Interfaces.Services.Logging
{
    public interface ILoggingService
    {
        public void Log(LogLevel level, string message, object logObject = null);
    }
}