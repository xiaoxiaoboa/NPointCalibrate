using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7.Net;
using WindowsFormsApp1.Enum;
using Timer = System.Timers.Timer;

namespace WindowsFormsApp1.Common {
    public class PlcControl {
        private static readonly Lazy<PlcControl> _instance = new Lazy<PlcControl>(() => new PlcControl());
        private Plc _plc;

        private Timer _timer;

        public bool IsConnected => _plc != null && _plc.IsConnected;

        public static PlcControl Instance => _instance.Value;

        // DB50
        public int FaultCode{ get; set; }
        public int MeasureNum{ get; set; }


        public int NineCaliNum{ get; set; }

        public int CenterCaliNum{ get; set; }
        public int CurrentControlMode{ get; set; }
        public float RealX{ get; set; }
        public float RealY{ get; set; }
        public float RealZ{ get; set; }

        public float RealR{ get; set; }

        // DB51
        public float OffsetR{ get; set; }
        public float OffsetX{ get; set; }
        public float OffsetY{ get; set; }
        public int MeasureNumCheck{ get; set; }
        public int CenterCaliNumCheck{ get; set; }
        public int FeedPosition{ get; set; }
        public int NineCaliNumCheck{ get; set; }
        public int ControlMode{ get; set; }


        private PlcControl() { }

        // 启动监听
        public void StartListener(int interval, Func<Task> callback) {
            if (!IsConnected) throw new Exception("PLC 未连接");

            _timer = new Timer();
            _timer.Interval = interval; // 每 1000ms 读取一次
            _timer.AutoReset = true;
            _timer.Elapsed += async (s, e) => await callback();
            _timer.Start();
        }

        public void StopListener() {
            _timer?.Stop();
        }


        // 连接
        public void Connect() {
            var ip = IniControl.Instance.Read("PLC", "IP");
            var port = IniControl.Instance.Read("PLC", "Port");
            if (_plc != null && _plc.IsConnected)
                return;
            _plc = new Plc(CpuType.S71500, ip, Convert.ToInt32(port), 0, 1);

            _plc.Open();
        }

        // 断开
        public void Disconnect() {
            if (_plc != null && _plc.IsConnected) {
                _plc.Close();
                _plc = null;
            }

            _timer?.Stop();
        }

        // 读plc
        public async Task<T> Read<T>(string address) {
            try {
                if (!IsConnected) throw new Exception("PLC未连接");
                var result = await _plc.ReadAsync(address);
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception exception) {
                throw new Exception($"读取数据失败：{exception.Message}");
            }
        }

        // 写plc
        public async Task Write(string address, object value) {
            try {
                if (!IsConnected) throw new InvalidOperationException("PLC 未连接");
                await _plc.WriteAsync(address, value);
            }
            catch (Exception exception) {
                throw new PlcException(ErrorCode.ReadData, "PLC读取数据失败", exception);
            }
        }
    }
}