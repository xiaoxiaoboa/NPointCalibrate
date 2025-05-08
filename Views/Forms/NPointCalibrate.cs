using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using S7.Net;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Views.Forms {
    public partial class NPointCalibrate : Form {
        private CogCalibNPointToNPointTool _caliTool;
        private const int TimerInterval = 1000;


        public NPointCalibrate() {
            InitializeComponent();
        }

        private void NPointCalibrate_Load(object sender, EventArgs e) {
            MyToolBlock.Instance.CalibrateToolBlock.Ran += CalibrateToolBlockOnRan;
            MyToolBlock.Instance.IdentificationToolBlock.Ran += IdentificationToolBlockOnRan;

            // 获取CogCalibNPointToNPointTool
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

        private void IdentificationToolBlockOnRan(object sender, EventArgs e) {

            if (MyToolBlock.Instance.IdentificationToolBlock.RunStatus.Result == CogToolResultConstants.Accept) {
                
            }
        }


        // 拍照之后 toolblock运行后的事件
        private async void CalibrateToolBlockOnRan(object sender, EventArgs e) {
            var serialNumber = PlcControl.Instance.NineCaliNum;
            // 获取toolblock处理后的record
            RunOnUIThread(() =>
                cogRecordDisplay1.Record = MyToolBlock.Instance.GetRecord(MyToolBlock.Instance.CalibrateToolBlock,
                    "CogFixtureTool1.OutputImage")
            );


            // 获取X Y
            var keys = new List<string> { "X", "Y" };
            var (res, values) = MyToolBlock.Instance.GetToolBlockOutputsResults(MyToolBlock.Instance.CalibrateToolBlock,
                keys);
            if (!res) {
                Logger.Instance.AddLog("标定ToolBlock结果不存在");
                return;
            }

            var x = (double)values["X"];
            var y = (double)values["Y"];


            Logger.Instance.AddLog($"像素坐标，x:{x},y:{y}");


            if (_caliTool == null) return;
            // 添加点对
            _caliTool.Calibration.AddPointPair(x, y, PlcControl.Instance.RealX, PlcControl.Instance.RealY);
            Logger.Instance.AddLog("添加点对成功：");
            Logger.Instance.AddLog(
                $"像素坐，X:{x}Y:{y}，机械坐标：X:{PlcControl.Instance.RealX},Y:{PlcControl.Instance.RealY}");

            try {
                // 写入九点矫正编号给plc,表示本次点对添加完成
                await PlcControl.Instance.Write(PlcDataAddress.NineCaliNumCheck.GetAddress(),
                    PlcControl.Instance.NineCaliNum);

                // 处理偏移
                await PlcControl.Instance.HandleOffset(PlcControl.Instance.MeasureNum);
            }
            catch (Exception exception) {
                Logger.Instance.AddLog($"偏移计算失败：{exception.Message}");
            }

            // 处理结果
            if (serialNumber == 9) {
                Logger.Instance.AddLog("*****已完成九个点对，开始计算结果：*****");

                try {
                    _caliTool.Calibration.Calibrate();
                }
                catch (Exception exception) {
                    Logger.Instance.AddLog($"计算结果失败：{exception.Message}");
                    return;
                }

                if (_caliTool.Calibration.Calibrated) {
                    Logger.Instance.AddLog($"RMS：{_caliTool.Calibration.ComputedRMSError}");
                    if (_caliTool.Calibration.ComputedRMSError > 30) {
                        Logger.Instance.AddLog("结果不理想，请重新标定");
                    }

                    Logger.Instance.AddLog("正在保存文件，请勿退出窗口...");
                    try {
                        // 保存
                        await Task.Run(() => {
                            MyToolBlock.Instance.SaveVpp(MyToolBlock.Instance.IdentificationToolBlock,
                                MyToolBlock.Instance.IdentificationVppPath);
                            // 重新加载识别vpp
                        });
                    }
                    catch (Exception exception) {
                        Logger.Instance.AddLog(exception.Message);
                        return;
                    }

                    // MyToolBlock.Instance.IdentificationToolBlock.Run();

                    Logger.Instance.AddLog("操作已完成");
                }
            }
        }


        // PLC九点标定触发事件
        private void OnNPointCalibrateChanged() {
            Logger.Instance.AddLog($"=====第{PlcControl.Instance.NineCaliNum}次到位，开始识别：=====");


            // 拍照
            var message = CameraControl.Instance.TakePhotoGraph();
            if (message != null) {
                Logger.Instance.AddLog(message);
            }
        }

        // 开启监听按钮
        private void listen_item_Click(object sender, EventArgs e) {
            try {
                PlcControl.Instance.StartListener(TimerInterval, NinePointCalibrateListening);
                listen_item.Enabled = false;
                Logger.Instance.AddLog($"监听PLC服务启动，每{TimerInterval}毫秒读取一次PLC");
            }
            catch (Exception exception) {
                Logger.Instance.AddLog($"监听未开启：{exception.Message}");
            }
        }

        // 窗口关闭
        private void NPointCalibrate_FormClosing(object sender, FormClosingEventArgs e) {
            MyToolBlock.Instance.CalibrateToolBlock.Ran -= CalibrateToolBlockOnRan;
            PlcControl.Instance.StopListener();
            Logger.Instance.AddLog("窗口事件已清除，窗口退出");
        }

        // 监听器执行的函数
        private async Task NinePointCalibrateListening() {
            try {
                var measureNum = await PlcControl.Instance.Read<int>(PlcDataAddress.MeasureNum.GetAddress());
                var x = await PlcControl.Instance.Read<uint>(PlcDataAddress.X.GetAddress());
                var y = await PlcControl.Instance.Read<uint>(PlcDataAddress.Y.GetAddress());
                PlcControl.Instance.MeasureNum = measureNum;
                PlcControl.Instance.RealX = x.ConvertToFloat();
                PlcControl.Instance.RealY = y.ConvertToFloat();

                var nineCaliNum = await PlcControl.Instance.Read<uint>(PlcDataAddress.NineCaliNum.GetAddress());
                // 限制
                if (PlcControl.Instance.NineCaliNum != nineCaliNum && nineCaliNum != 0) {
                    PlcControl.Instance.NineCaliNum = nineCaliNum.ConvertToInt();
                    // 读取到数据后执行拍照
                    OnNPointCalibrateChanged();
                }
            }
            catch (Exception exception) {
                Logger.Instance.AddLog($"{exception.Message}", LogLevel.Error);
                PlcControl.Instance.StopListener();
                Logger.Instance.AddLog("监听服务终止");

                listen_item.Enabled = false;
                clear_item.Enabled = false;
            }
        }

        // 清除
        private void clear_item_Click(object sender, EventArgs e) {
            PlcControl.Instance.NineCaliNum = 0;
            PlcControl.Instance.NineCaliNumCheck = 0;
            PlcControl.Instance.RealX = 0;
            PlcControl.Instance.RealY = 0;

            listen_item.Enabled = true;
            PlcControl.Instance.StopListener();
            Logger.Instance.AddLog("PLC监听停止");


            while (_caliTool.Calibration.NumPoints > 0) {
                _caliTool.Calibration.DeletePointPair(0);
            }

            // 保存一下
            MyToolBlock.Instance.SaveVpp(MyToolBlock.Instance.IdentificationToolBlock,
                MyToolBlock.Instance.IdentificationVppPath);

            Logger.Instance.AddLog("点位已清除");
        }
    }
}