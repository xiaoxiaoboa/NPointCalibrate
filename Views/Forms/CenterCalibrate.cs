using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Cognex.VisionPro.CalibFix;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Views.Forms {
    public partial class CenterCalibrate : Form {
        private CogCalibNPointToNPointTool _caliTool;
        private readonly Dictionary<int, PointF> _calibrationPoints = new Dictionary<int, PointF>();

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

        private async void IdentificationToolBlockOnRan(object sender, EventArgs e) {
            var serialNumber = PlcControl.Instance.NineCaliNum;
            RunOnUIThread(() =>
                cogRecordDisplay1.Record = MyToolBlock.Instance.IdentificationToolBlock.CreateLastRunRecord()
                    .SubRecords["CogFixtureTool1"]
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
            
            // 向PLC写入确认信息
            await PlcControl.Instance.Write(PlcDataAddress.CenterCaliNumCheck.GetAddress(),
                PlcControl.Instance.CenterCaliNum);

            if (serialNumber != 3) return;
            
            try {
                var point = CalculateCircleCenter(_calibrationPoints[0], _calibrationPoints[1],
                    _calibrationPoints[2]);
                // 存圆心坐标
                // _calibrationPoints.Add(3, point);
                Logger.Instance.AddLog($"圆心计算完成，x：{point.X}，y：{point.Y}");
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message);
            }
        }

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


        public void OnCenterCalibrateChanged() {
            Logger.Instance.AddLog($"=====第{PlcControl.Instance.NineCaliNum}次到位，开始执行：=====");
            CameraControl.Instance.TakePhotoGraph();
        }

        private void save_item_Click(object sender, EventArgs e) {
            
        }

        private void result_item_Click(object sender, EventArgs e) {
            throw new System.NotImplementedException();
        }
    }
}