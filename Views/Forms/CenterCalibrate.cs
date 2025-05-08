using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.CalibFix;
using S7.Net;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Views.Forms {
    public partial class CenterCalibrate : Form {
        private CogCalibNPointToNPointTool _caliTool;
        private readonly Dictionary<int, PointF> _calibrationPoints = new Dictionary<int, PointF>();
        private const int TimerInterval = 1000;

        public CenterCalibrate() {
            InitializeComponent();
        }

        private void CenterCalibrate_Load(object sender, EventArgs e) {
            MyToolBlock.Instance.IdentificationToolBlock.Ran += IdentificationToolBlockOnRan;
            _caliTool = MyToolBlock.Instance.GetTool<CogCalibNPointToNPointTool>(
                MyToolBlock.Instance.IdentificationToolBlock,
                "CogCalibNPointToNPointTool1");
        }

        // 恢复控件到UI线程执行
        private void RunOnUIThread(Action action) {
            if (InvokeRequired) {
                Invoke(action);
            }
            else {
                action();
            }
        }

        // 识别ToolBlock执行后
        private async void IdentificationToolBlockOnRan(object sender, EventArgs e) {
            var serialNumber = PlcControl.Instance.CenterCaliNum;
            RunOnUIThread(() =>
                cogRecordDisplay1.Record =
                    MyToolBlock.Instance.GetRecord(MyToolBlock.Instance.IdentificationToolBlock, "CogFixtureTool1")
            );

            var keys = new List<string> { "X", "Y" };
            var (res, values) =
                MyToolBlock.Instance.GetToolBlockOutputsResults(MyToolBlock.Instance.IdentificationToolBlock, keys);
            if (!res) {
                Logger.Instance.AddLog("识别ToolBlock结果不存在");
                return;
            }

            var x = (float)values["X"];
            var y = (float)values["Y"];

            if (_caliTool == null) return;

            if (serialNumber <= 3) {
                _calibrationPoints.Add(serialNumber, new PointF(x, y));
                Logger.Instance.AddLog($"{serialNumber}次坐标获取完成");
            }

            try {
                // 向PLC写入确认信息
                await PlcControl.Instance.Write(PlcDataAddress.CenterCaliNumCheck.GetAddress(),
                    PlcControl.Instance.CenterCaliNum);
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message);
            }

            if (serialNumber != 3) return;

            try {
                Logger.Instance.AddLog("正在计算...");
                var point = CalculateCircleCenter(_calibrationPoints[0], _calibrationPoints[1],
                    _calibrationPoints[2]);
                // 存圆心坐标
                IniControl.Instance.RotateCenterX = point.X;
                IniControl.Instance.RotateCenterY = point.Y;
                Logger.Instance.AddLog($"圆心计算完成，x：{point.X}，y：{point.Y}");

                listen_item.Enabled = true;
                result_item.Enabled = true;
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message);
            }
        }

        // 计算
        private static PointF CalculateCircleCenter(PointF p1, PointF p2, PointF p3) {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double c = p1.X - p3.X;
            double d = p1.Y - p3.Y;
            double e = ((p1.X * p1.X - p2.X * p2.X) - (p2.Y * p2.Y - p1.Y * p1.Y)) / 2;
            double f = ((p1.X * p1.X - p3.X * p3.X) - (p3.Y * p3.Y - p1.Y * p1.Y)) / 2;

            double denominator = a * d - b * c;
            if (Math.Abs(denominator) < 1e-6)
                throw new InvalidOperationException("三点共线，无法计算圆心");


            double x = (e * d - b * f) / denominator;
            double y = (a * f - e * c) / denominator;

            return new PointF((float)x, (float)y);
        }

        // 监听器执行的函数
        private async Task CenterCalibrateListening() {
            try {
                var centerCaliNum = await PlcControl.Instance.Read<int>(PlcDataAddress.CenterCaliNum.GetAddress());
                if (centerCaliNum != PlcControl.Instance.CenterCaliNum && centerCaliNum != 0) {
                    PlcControl.Instance.CenterCaliNum = centerCaliNum;

                    // 读取到数据后执行拍照
                    OnCenterCalibrateChanged();
                }
            }
            catch (Exception exception) {
                Logger.Instance.AddLog($"{exception.Message}", LogLevel.Error);
                PlcControl.Instance.StopListener();
                Logger.Instance.AddLog("监听服务终止");
            }
        }

        private void OnCenterCalibrateChanged() {
            Logger.Instance.AddLog($"=====第{PlcControl.Instance.CenterCaliNum}次到位，开始执行：=====");
            CameraControl.Instance.TakePhotoGraph();
        }


        // 开启监听
        private void listen_item_Click(object sender, EventArgs e) {
            try {
                PlcControl.Instance.StartListener(TimerInterval, CenterCalibrateListening);
                Logger.Instance.AddLog($"监听PLC服务启动，每{TimerInterval}毫秒读取一次PLC");
                listen_item.Enabled = false;
            }
            catch (Exception exception) {
                Logger.Instance.AddLog($"监听未开启：{exception.Message}");
            }
        }


        private void result_item_Click(object sender, EventArgs e) {
            var centerCalibrateResult = new CenterCalibrateResults(_calibrationPoints);
            centerCalibrateResult.Show();
        }

        private void CenterCalibrate_FormClosing(object sender, FormClosingEventArgs e) {
            PlcControl.Instance.CenterCaliNum = 0;
            PlcControl.Instance.CenterCaliNumCheck = 0;
            PlcControl.Instance.StopListener();
            Logger.Instance.AddLog("PLC监听停止");
        }
    }
}