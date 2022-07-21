using NLog;

namespace Infrastructure.CrossCutting.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {

        }

        public void LoggerWarn(string message)
        {
            Logger.Warn(message);
        }

        public void LoggerInfo(string message)
        {
            Logger.Info(message);
        }

        public void LoggerDebug(string message)
        {
            Logger.Debug(message);
        }

        public void LoggerError(string message)
        {
            Logger.Error(message);
        }

    }
}
