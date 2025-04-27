using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using S7.Net;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;

namespace WindowsFormsApp1.Views.Forms {
    public partial class NPointCalibrate : Form {
        public NPointCalibrate() {
            InitializeComponent();
            MyToolBlock.Instance.CalibrateToolBlock.Ran += CalibrateToolBlockOnRan;
            MyToolBlock.Instance.IdentificationToolBlock.Ran += IdentificationToolBlockOnRan;
        }

        private void IdentificationToolBlockOnRan(object sender, EventArgs e) {
            Logger.Instance.AddLog("准备获取识别ToolBlock结果，向PLC发送...");
            var keys = new List<string>() { "Angle", "X", "Y" };
            var (res, values) =
                MyToolBlock.Instance.GetToolBlockOutputsResults(MyToolBlock.Instance.IdentificationToolBlock, keys);

            var angle = (float)values["Angle"];
            var x = (float)values["X"];
            var y = (float)values["Y"];
            
        }

        // 拍照之后 toolblock运行后的事件
        private void CalibrateToolBlockOnRan(object sender, EventArgs e) {
            // 获取toolblock处理后的record
            cogRecordDisplay1.Record = MyToolBlock.Instance.CalibrateToolBlock.CreateLastRunRecord()
                .SubRecords["CogImageConvertTool1.OutputImage"];

            // 获取X Y
            var keys = new List<string> { "X", "Y" };
            var (res, values) = MyToolBlock.Instance.GetToolBlockOutputsResults(MyToolBlock.Instance.CalibrateToolBlock,
                keys);
            if (!res) {
                Logger.Instance.AddLog("ToolBlock结果不存在");
                return;
            }

            var x = (double)values["X"];
            var y = (double)values["Y"];


            Logger.Instance.AddLog($"像素坐标，x:{x},y:{y}");


            // 获取CogCalibNPointToNPointTool
            var tool = MyToolBlock.Instance.GetTool<CogCalibNPointToNPointTool>(
                MyToolBlock.Instance.IdentificationToolBlock,
                "CogCalibNPointToNPointTool1");
            if (tool != null) {
                // 添加点对
                tool.Calibration.AddPointPair(x, y, PlcControl.Instance.RealX, PlcControl.Instance.RealY);
                Logger.Instance.AddLog("添加点对成功：");
                Logger.Instance.AddLog(
                    $"像素坐，X:{x}Y:{y}，机械坐标：X:{PlcControl.Instance.RealX},Y:{PlcControl.Instance.RealY}");
            }
        }


        // PLC九点标定触发事件
        private void OnOnNPointCalibrateChanged(int serialNumber) {
            Logger.Instance.AddLog($"=====第{serialNumber}次到位，开始识别：=====");

            // 拍照
            var image = CameraControl.Instance.TakePhotoGraph();
        }

        // 开启监听按钮
        private void listen_item_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("先清除监听事件");
            PlcControl.Instance.OnNPointCalibrateChanged -= OnOnNPointCalibrateChanged;
            PlcControl.Instance.OnNPointCalibrateChanged += OnOnNPointCalibrateChanged;
            Logger.Instance.AddLog("监听事件启动");

            OnOnNPointCalibrateChanged(1);
        }

        private void NPointCalibrate_FormClosing(object sender, FormClosingEventArgs e) {
            MyToolBlock.Instance.CalibrateToolBlock.Ran -= CalibrateToolBlockOnRan;
            PlcControl.Instance.OnNPointCalibrateChanged -= OnOnNPointCalibrateChanged;
        }
    }
}