using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1 {
    public interface ILog {
        string TimeStamp { get; }
        LogLevel Level { get; }
        string Message { get; }
    }
}