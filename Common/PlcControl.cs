using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7.Net;
using WindowsFormsApp1.Enum;
using Timer = System.Timers.Timer;

namespace WindowsFormsApp1.Common {
    public class PlcControl {
        private const string IpAddress = "192.168.136.6";
        private const int Port = 102;
        private static readonly Lazy<PlcControl> _instance = new Lazy<PlcControl>(() => new PlcControl());
        private Plc _plc;

        private Timer _timer;

        public bool IsConnected => _plc != null && _plc.IsConnected;

        public static PlcControl Instance => _instance.Value;

        public event Action<int> OnNPointCalibrateChanged;

        private PlcControl() { }

        // 计时器
        private void StartListening() {
            _timer = new Timer();
            _timer.Interval = 1000; // 每 1000ms 读取一次
            _timer.AutoReset = true;
            _timer.Elapsed += async (s, e) => await CheckButtonAsync();
            _timer.Start();
            Logger.Instance.AddLog($"监听PLC服务启动，每{_timer.Interval}毫秒执行一次");
        }


        public void Connect() {
            if (_plc != null && _plc.IsConnected)
                return;
            _plc = new Plc(CpuType.S71500, IpAddress, Port, 0, 1);

            _plc.Open();

        }

        public void Disconnect() {
            if (_plc != null && _plc.IsConnected) {
                _plc.Close();
                _plc = null;
            }

            _timer?.Stop();
        }

        public async Task<T> Read<T>(string address) {
            try {
                if (!IsConnected) throw new InvalidOperationException("PLC 未连接");
                var result = await _plc.ReadAsync(address);
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception exception) {
                throw new PlcException(ErrorCode.ReadData, "PLC读取数据失败", exception);
            }
        }

        public async Task Write(string address, object value) {
            try {
                if (!IsConnected) throw new InvalidOperationException("PLC 未连接");
                await _plc.WriteAsync(address, value);
            }
            catch (Exception exception) {
                throw new PlcException(ErrorCode.ReadData, "PLC读取数据失败", exception);
            }
        }

        private async Task CheckButtonAsync() {
            try {
                var result = await Read<int>(PlcDataAddress.Press.GetAddress()); // 按钮位地址
                if (result == 0) {
                    //触发
                }
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message);
            }
        }
    }
}