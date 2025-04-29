using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using S7.Net;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;
using WindowsFormsApp1.Interface;
using WindowsFormsApp1.Views.Forms;

namespace WindowsFormsApp1 {
    public partial class Main : Form {
        #region 属性

        // 控制子控件放大缩小的相关属性
        private readonly Dictionary<Control, ChildControlInfo> _controlInfos =
            new Dictionary<Control, ChildControlInfo>();

        private ChildControlInfo _childControlInfo;


        // toolblock vpp
        private readonly string _calibrateVppPath = Path.Combine(Application.StartupPath, "vpp/calibrate.vpp");

        private readonly string _calibNPointToNPointVppPath =
            Path.Combine(Application.StartupPath, "vpp/calibNPointToNPoint.vpp");

        // 登录窗口事件
        public event EventHandler OnMainFormClose;

        #endregion

        public Main() {
            InitializeComponent();
            InitializeChildControls();

            // 初始化listview
            Logger.Instance.Logs.ListChanged += LogListOnListChanged;
            listView1.Columns.Add("时间戳");
            listView1.Columns.Add("日志等级");
            listView1.Columns.Add("信息");
            Logger.Instance.AddLog("程序启动");
            Logger.Instance.AddLog("123132,342352423,42312123,123124ggbcvbgdf");
        }

        #region 方法

        // 加载vpp
        private async Task LoadVpp(string toolBlockVppPath, LoadToolBlock loadToolBlock) {
            Logger.Instance.AddLog("加载ToolBlock vpp...");

            var (message, logLevel) = await MyToolBlock.Instance.LoadToolBlockVpp(toolBlockVppPath, loadToolBlock);

            Logger.Instance.AddLog(message, logLevel);
        }

        // 初始化相机
        private async void InitCamera() {
            initCameraMenu_item.Enabled = false;
            initCamera.Enabled = false;
            string errorMessage;

            Logger.Instance.AddLog("相机初始化中...");


            errorMessage = await CameraControl.Instance.Initialize();

            if (errorMessage != null) {
                Logger.Instance.AddLog($@"相机连接错误：{errorMessage}", LogLevel.Error);
                initCamera.Enabled = true;
                initCameraMenu_item.Enabled = true;

                return;
            }

            indicatorLight1.IsOn = true;
            startLive.Enabled = true;
            takePho.Enabled = true;
            disconnectCamera_item.Enabled = true;

            Logger.Instance.AddLog("相机初始化完成");

            // 绑定拍照完成事件
            CameraControl.Instance.Acq.Complete += Complete;
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

        // 获取toolblock处理后的图像给recorddisplay
        private void UpdateRecordDisplay(
            MyRecordDisplay display,
            CogToolBlock toolBlock
        ) {
            display.Invoke((MethodInvoker)delegate { display.SetRecord(toolBlock.CreateLastRunRecord()); });

            Logger.Instance.AddLog("图像处理完成，已加载到标定RecordDisplay");
        }

        #endregion

        #region 事件

        // listview更新事件
        private void LogListOnListChanged(object sender, ListChangedEventArgs e) {
            var log = Logger.Instance.Logs.LastOrDefault();
            var item = new ListViewItem(log?.TimeStamp);

            RunOnUIThread(() => {
                item.SubItems.Add(log?.Level.ToString());
                item.SubItems.Add(log?.Message);
                listView1.Items.Add(item);
                listView1.Items[listView1.Items.Count - 1].EnsureVisible();
            });
        }

        #region 控制cogDisplay放大缩小

        // 初始化子控件控制
        private void InitializeChildControls() {
            RegisterControl(myDisplay1, panel5, "实时预览");
            RegisterControl(myDisplay2, panel6, "一次拍照");
            RegisterControl(myRecordDisplay1, panel7, "标定RecordDisplay");
            RegisterControl(myRecordDisplay2, panel10, "识别RecordDisplay");
            RegisterControl(listView1, panel11);
        }

        // 注册子控件事件
        private void RegisterControl(Control control, Panel parent, string labelText = "") {
            var info = new ChildControlInfo(control, parent, control.Dock);
            _controlInfos.Add(control, info);

            if (control is IMyControl display) {
                display.SetLabelText(labelText);
                display.MaximizeRestoreRequested += UserControl_MaximizeRestoreRequested;
            }
        }

        // 控制事件
        private void UserControl_MaximizeRestoreRequested(object sender, EventArgs e) {
            var requestedControl = sender as Control;
            if (requestedControl == null || !_controlInfos.TryGetValue(requestedControl, out var requestedInfo)) {
                return; // 无效的请求
            }


            if (_childControlInfo == null) {
                MaximizeControl(requestedInfo);
            }

            else if (_childControlInfo == requestedInfo) {
                RestoreControl(requestedInfo);
            }
        }

        // 最大化控制
        private void MaximizeControl(ChildControlInfo info) {
            if (info == null) return;

            _childControlInfo = info; // 记录哪个被最大化了
            // 状态
            info.Status = ChildControlStatus.Maximized;

            // 改变父控件（放大）
            info.ControlInstance.Parent = panel4;

            // 填满（放大）
            info.ControlInstance.Dock = DockStyle.Fill;

            info.ControlInstance.BringToFront();
        }

        // 还原控件的逻辑
        private void RestoreControl(ChildControlInfo info) {
            if (info == null) return;

            // 状态
            info.Status = ChildControlStatus.Minimized;

            // 清空
            _childControlInfo = null; // 清除最大化记录


            info.ControlInstance.Dock = info.OriginalDockStyle;

            // 改回原来的父控件
            info.ControlInstance.Parent = info.OriginalParent;
        }

        #endregion

        // 事件：toolblock 执行运行后
        private void OnCalibrateToolBlockRan(object sender, EventArgs e) {
            UpdateRecordDisplay(myRecordDisplay1, MyToolBlock.Instance.CalibrateToolBlock);
        }

        private void OnIdentificationToolBlockRan(object sender, EventArgs e) {
            // UpdateRecordDisplay(myRecordDisplay2, MyToolBlock.Instance.IdentificationToolBlock);
            // Logger.Instance.AddLog("准备获取识别ToolBlock结果，向PLC发送...");
            // var keys = new List<string>() { "Angle", "X", "Y" };
            // var (res, values) =
            //     MyToolBlock.Instance.GetToolBlockOutputsResults(MyToolBlock.Instance.IdentificationToolBlock, keys);
            //
            // var angle = (float)values["Angle"];
            // var x = (float)values["X"];
            // var y = (float)values["Y"];
            //
            // // 弧度转角度
            // angle = (float)(180 / Math.PI) * angle;
            // float offsetR = angle * -1;
            // PlcControl.Instance.OffsetR = offsetR;
            //
            // float offsetX = x * -1;
            // float offsetY = y * -1;
            //
            // PlcControl.Instance.OffsetX = offsetX;
            // PlcControl.Instance.OffsetY = offsetY;
            //
            // Logger.Instance.AddLog($"x偏移：{offsetX}，y偏移：{offsetY}，r偏移：{offsetR}");
        }

        // 传图像给toolblock
        private void ImageToToolBlock(CogToolBlock toolBlock, ICogImage image, Action callback = null) {
            if (toolBlock != null) {
                if (!toolBlock.Inputs.Contains("InputImage")) {
                    toolBlock.Inputs.Add(new CogToolBlockTerminal("InputImage", image));
                }

                toolBlock.Inputs["InputImage"].Value = image;


                // 传进图像后，执行toolblock
                callback?.Invoke();
            }
            else {
                Logger.Instance.AddLog("ToolBlock vpp文件未加载，图像不会被处理");
            }
        }

        // 拍照完成事件
        private void Complete(object sender, CogCompleteEventArgs e) {
            try {
                var image = CameraControl.Instance.GetGraphic();
                RunOnUIThread(() => { myDisplay2.SetGraphic(image); });

                ImageToToolBlock(MyToolBlock.Instance.CalibrateToolBlock, image,
                    () => MyToolBlock.Instance.CalibrateToolBlock.Run());
                ImageToToolBlock(MyToolBlock.Instance.IdentificationToolBlock, image);


                CameraControl.Instance.IsShooting = false;
                RunOnUIThread(() => { takePho.Enabled = true; });
                Logger.Instance.AddLog("拍照结束");
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message, LogLevel.Error);
            }
        }


        // 开启实时
        private void startLive_Click(object sender, EventArgs e) {
            myDisplay1.StartLive(CameraControl.Instance.Acq);

            Logger.Instance.AddLog("开启相机实时预览");

            startLive.Enabled = false;
            stopLive_item.Enabled = true;
        }

        // 关闭实时
        private void stopLive_Click(object sender, EventArgs e) {
            myDisplay1.StopLive();

            Logger.Instance.AddLog("关闭相机实时预览");

            stopLive_item.Enabled = false;
            startLive.Enabled = true;
        }

        // 一次拍照
        private void takePho_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("开始拍照...");
            takePho.Enabled = false;

            var message = CameraControl.Instance.TakePhotoGraph();
            Logger.Instance.AddLog(message);
        }

        // plc连接
        private async void connect_plc_Click(object sender, EventArgs e) {
            var message = await Task.Run(() => {
                try {
                    PlcControl.Instance.Connect();

                    if (PlcControl.Instance.IsConnected) {
                        RunOnUIThread(() => {
                            connectPlc.Enabled = false;
                            connectPlc_item.Enabled = false;
                            disconnectPlc_item.Enabled = true;
                            indicatorLight2.IsOn = true;
                        });
                        return "PLC连接成功";
                    }

                    throw new Exception("PLC连接失败");
                }
                catch (Exception exception) {
                    return exception.Message;
                }
            });
            Logger.Instance.AddLog(message);
        }

        // plc断开
        private void disconnect_plc_Click(object sender, EventArgs e) {
            PlcControl.Instance.Disconnect();

            connectPlc.Enabled = true;
            connectPlc_item.Enabled = true;
            disconnectPlc_item.Enabled = false;
            indicatorLight2.IsOn = false;
            Logger.Instance.AddLog("PLC断开连接");
        }

        // 初始化相机事件
        private void init_camera_btn_Click(object sender, EventArgs e) {
            InitCamera();
        }

        // 右键控制listview放大缩小按钮
        private void maxControl_Click(object sender, EventArgs e) {
            _controlInfos.TryGetValue(listView1, out var lv);

            if (lv?.Status == ChildControlStatus.Maximized) {
                maxControl.Text = @"放大";
            }
            else if (lv?.Status == ChildControlStatus.Minimized) {
                maxControl.Text = @"缩小";
            }

            UserControl_MaximizeRestoreRequested(listView1, e);
        }

        // 导出日志CSV
        private async void exportCsv_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("正在导出日志...");

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"CSV文件 (*.csv)|*.csv";
            saveFileDialog.FileName = "Log.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                var message = await Logger.Instance.ExportToCsv(saveFileDialog.FileName);
                Logger.Instance.AddLog(message);
            }
            else {
                Logger.Instance.AddLog("取消导出日志");
            }
        }


        // 初始化相机事件
        private void initCamera_menuItem_Click(object sender, EventArgs e) {
            InitCamera();

            initCameraMenu_item.Enabled = false;
        }

        // 断开相机
        private void disconnectCamera_item_Click(object sender, EventArgs e) {
            CameraControl.Instance.DisposeCamera();

            if (myDisplay1.LiveDisplayStatus()) {
                myDisplay1.StopLive();
            }

            startLive.Enabled = false;
            stopLive_item.Enabled = false;
            takePho.Enabled = false;
            disconnectCamera_item.Enabled = false;
            initCameraMenu_item.Enabled = true;
            initCamera.Enabled = true;
            indicatorLight1.IsOn = false;
            Logger.Instance.AddLog("相机已断开...");
        }

        // 开启标定作业窗口
        private void calibrate_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("打开标定作业Form...");
            var calibrateForm = new ToolBlock(MyToolBlock.Instance.CalibrateToolBlock, LoadToolBlock.Calibrate);
            calibrateForm.Show();
        }

        // 开启识别作业窗口
        private void identificationWork_item_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("打开识别作业Form...");
            var identificationForm =
                new ToolBlock(MyToolBlock.Instance.IdentificationToolBlock, LoadToolBlock.Identification);
            identificationForm.Show();
        }

        // 关闭窗口
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            // 关闭实时预览
            if (myDisplay1.LiveDisplayStatus()) {
                myDisplay1.StopLive();
            }

            // 销毁相机实例
            CameraControl.Instance.DisposeCamera();
            // 断开plc
            PlcControl.Instance.Disconnect();

            Logger.Instance.Logs.ListChanged -= LogListOnListChanged;

            if (CameraControl.Instance.Acq != null) {
                CameraControl.Instance.Acq.Complete -= Complete;
            }

            if (MyToolBlock.Instance.CalibrateToolBlock != null)
                MyToolBlock.Instance.CalibrateToolBlock.Ran -= OnCalibrateToolBlockRan;
            if (MyToolBlock.Instance.IdentificationToolBlock != null)
                MyToolBlock.Instance.IdentificationToolBlock.Ran -= OnIdentificationToolBlockRan;

            // 关闭main窗口时，也关闭登录窗口
            OnMainFormClose?.Invoke(this, EventArgs.Empty);
        }

        // 加载标定作业tb vpp
        private async void calibrate_item_Click(object sender, EventArgs e) {
            await LoadVpp(_calibrateVppPath, LoadToolBlock.Calibrate);
            MyToolBlock.Instance.CalibrateToolBlock.Ran -= OnCalibrateToolBlockRan;
            MyToolBlock.Instance.CalibrateToolBlock.Ran += OnCalibrateToolBlockRan;
        }

        // 加载识别作业tb vpp
        private async void identification_item_Click(object sender, EventArgs e) {
            await LoadVpp(_calibNPointToNPointVppPath, LoadToolBlock.Identification);
            MyToolBlock.Instance.IdentificationToolBlock.Ran -= OnIdentificationToolBlockRan;
            MyToolBlock.Instance.IdentificationToolBlock.Ran += OnIdentificationToolBlockRan;
        }

        // 标定和识别作业同时加载
        private async void tbBoth_item_Click(object sender, EventArgs e) {
            await LoadVpp(_calibrateVppPath, LoadToolBlock.Calibrate);
            await LoadVpp(_calibNPointToNPointVppPath, LoadToolBlock.Identification);

            MyToolBlock.Instance.CalibrateToolBlock.Ran -= OnCalibrateToolBlockRan;
            MyToolBlock.Instance.CalibrateToolBlock.Ran += OnCalibrateToolBlockRan;
            MyToolBlock.Instance.IdentificationToolBlock.Ran -= OnIdentificationToolBlockRan;
            MyToolBlock.Instance.IdentificationToolBlock.Ran += OnIdentificationToolBlockRan;
        }

        // 打开九点标定窗口
        private void ninePointCali_item_Click(object sender, EventArgs e) {
            if (MyToolBlock.Instance.CalibrateToolBlock == null ||
                MyToolBlock.Instance.IdentificationToolBlock == null) {
                Logger.Instance.AddLog("标定和识别ToolBlock未加载！！！");
                return;
            }

            Logger.Instance.AddLog("打开九点标定窗口");
            var ninePoint = new NPointCalibrate();
            ninePoint.Show();
        }

        #endregion
    }
}