namespace Infrastructure.CrossCutting.Logger
{
    public interface ILoggerManager
    {
        void LoggerWarn(string message);
        void LoggerInfo(string message);
        void LoggerDebug(string message);
        void LoggerError(string message);

    }
}