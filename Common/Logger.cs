using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Common {
    public class Logger {
        private static readonly Logger _instance = new Logger();
        private readonly BindingList<ILog> _bindingLogList = new BindingList<ILog>();
        private readonly object _lock = new object();

        public static Logger Instance => _instance;

        public BindingList<ILog> Logs{
            get {
                lock (_lock) {
                    return _bindingLogList;
                }
            }
        }

        private Logger() { }

        // 添加日志
        public void AddLog(string message, LogLevel level = LogLevel.Info) {
            if (message == null) return;
            // 格式化时间字符串
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            lock (_lock) {
                _bindingLogList.Add(new Log(timestamp, level, message));
            }
        }

        // 导出
        public async Task<string> ExportToCsv(string filename) {
            List<ILog> logsCopy;
            lock (_lock) {
                logsCopy = new List<ILog>(_bindingLogList);
            }

            try {
                using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8)) {
                    await sw.WriteLineAsync("时间,等级,信息");

                    foreach (var log in logsCopy) {
                        await sw.WriteLineAsync($"{log.TimeStamp},{log.Level},{log.Message}");
                    }
                }

                return "导出日志成功";
            }
            catch (Exception exception) {
                return $"导出日志失败：{exception.Message}";
            }
        }
    }
}