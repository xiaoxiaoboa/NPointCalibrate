using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.CalibFix;
using S7.Net;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;

namespace WindowsFormsApp1.Views.Forms {
    public partial class NPointCalibrate : Form {
        public NPointCalibrate() {
            InitializeComponent();
        }

        private async void OnOnNPointCalibrateChanged(int serialNumber) {
            Logger.Instance.AddLog($"=====第{serialNumber}次到位，开始识别：=====");

            await Task.Delay(1000);

            MyToolBlock.Instance.CalibrateToolBlock.Run();

            // 获取X Y
            var res = MyToolBlock.Instance.GetCalibrateResult(out double x, out double y);
            if (!res) {
                Logger.Instance.AddLog("ToolBlock结果不存在");
                return;
            }
            else {
                Logger.Instance.AddLog($"像素坐标，x:{x},y:{y}");
            }
            // 获取CogCalibNPointToNPointTool
            var tool = MyToolBlock.Instance.GetTool<CogCalibNPointToNPointTool>(
                MyToolBlock.Instance.IdentificationToolBlock,
                "CogCalibNPointToNPointTool1");
            if (tool != null) {
                // 添加点对
                tool.Calibration.AddPointPair(x, y, PlcControl.Instance.RealX, PlcControl.Instance.RealY);
                Logger.Instance.AddLog("添加点对成功：");
                Logger.Instance.AddLog($"像素坐，X:{x}Y:{y}，机械坐标：X:{PlcControl.Instance.RealX},Y:{PlcControl.Instance.RealY}");
            }
        }

        private void listen_item_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("先清除监听事件");
            PlcControl.Instance.OnNPointCalibrateChanged -= OnOnNPointCalibrateChanged;
            PlcControl.Instance.OnNPointCalibrateChanged += OnOnNPointCalibrateChanged;
            Logger.Instance.AddLog("监听事件启动");

            OnOnNPointCalibrateChanged(1);
        }
    }
}