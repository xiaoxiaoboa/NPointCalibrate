using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;
using WindowsFormsApp1.Interface;
using WindowsFormsApp1.Views;
using WindowsFormsApp1.Views.Forms;

namespace WindowsFormsApp1 {
    public partial class Main : Form {
        #region 属性

        private MyToolBlock _calibrateTb = new MyToolBlock();
        private MyToolBlock _calibNPointToNPointVppPathTb = new MyToolBlock();

        // 相机相关属性
        private readonly CameraControl _cameraControl = new CameraControl();


        // 控制子控件放大缩小的相关属性
        private readonly Dictionary<Control, ChildControlInfo> _controlInfos =
            new Dictionary<Control, ChildControlInfo>();

        private ChildControlInfo _childControlInfo;


        // toolblock vpp
        private readonly string _calibrateVppPath = Path.Combine(Application.StartupPath, "vpp/calibrate.vpp");

        private readonly string _calibNPointToNPointVppPath =
            Path.Combine(Application.StartupPath, "vpp/calibNPointToNPoint.vpp");

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
        }

        #region 方法

        // 加载vpp
        private async Task LoadVpp(string toolBlockVppPath, MyToolBlock toolBlock) {
            Logger.Instance.AddLog("加载ToolBlock vpp...");

            var (message, logLevel) = await toolBlock.LoadToolBlockVpp(toolBlockVppPath);

            Logger.Instance.AddLog(message, logLevel);
        }

        // 传图像给toolblock
        private void ImageToToolBlock(MyToolBlock myToolBlock, ICogImage image) {
            if (myToolBlock.ToolBlock != null) {
                if (!myToolBlock.ToolBlock.Inputs.Contains("InputImage")) {
                    myToolBlock.ToolBlock.Inputs.Add(new CogToolBlockTerminal("InputImage", image));
                }

                myToolBlock.ToolBlock.Inputs["InputImage"].Value = image;

                //图像已加载到ToolBlock，开始处理图像...
                myToolBlock.ToolBlock.Run();
            }
            else {
                Logger.Instance.AddLog("ToolBlock vpp文件未加载，图像不会被处理");
            }
        }

        // 初始化相机
        private async void InitCamera() {
            initCameraMenu_item.Enabled = false;
            initCamera.Enabled = false;
            string errorMessage = null;

            Logger.Instance.AddLog("相机初始化中...");


            errorMessage = await _cameraControl.Initialize();

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
            _cameraControl.Acq.Complete += Complete;
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


        // 拍照完成事件
        private void Complete(object sender, CogCompleteEventArgs e) {
            try {
                var image = _cameraControl.GetGraphic();
                RunOnUIThread(() => { myDisplay2.SetGraphic(image); });


                // 传图像给toolblock
                ImageToToolBlock(_calibrateTb, image);
                ImageToToolBlock(_calibNPointToNPointVppPathTb, image);

                _cameraControl.IsShooting = false;
                RunOnUIThread(() => { takePho.Enabled = true; });
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message, LogLevel.Error);
            }
        }


        // 开启实时
        private void startLive_Click(object sender, EventArgs e) {
            myDisplay1.StartLive(_cameraControl.Acq);

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
            Logger.Instance.AddLog("开始一次拍照...");
            takePho.Enabled = false;

            var message = _cameraControl.TakePhotoGraph();
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
            _cameraControl.DisposeCamera();

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
            var calibrateForm = new ToolBlock(_calibrateTb);
            calibrateForm.Show();
        }

        // 开启识别作业窗口
        private void identificationWork_item_Click(object sender, EventArgs e) {
            Logger.Instance.AddLog("打开识别作业Frorm...");
            var calibNPointToNPointForm = new ToolBlock(_calibNPointToNPointVppPathTb);
            calibNPointToNPointForm.Show();
        }

        // 工具栏导出日志按钮事件
        private void exportLog_Click(object sender, EventArgs e) {
            exportCsv_Click(sender, e);
        }

        // 关闭窗口
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            // 关闭实时预览
            if (myDisplay1.LiveDisplayStatus()) {
                myDisplay1.StopLive();
            }

            // 销毁相机实例
            _cameraControl.DisposeCamera();
            // 断开plc
            PlcControl.Instance.Disconnect();

            Logger.Instance.Logs.ListChanged -= LogListOnListChanged;

            if (_cameraControl.Acq != null) {
                _cameraControl.Acq.Complete -= Complete;
            }
        }

        // 加载标定作业tb vpp
        private async void calibrate_item_Click(object sender, EventArgs e) {
            await LoadVpp(_calibrateVppPath, _calibrateTb);
        }

        // 加载识别作业tb vpp
        private async void identification_item_Click(object sender, EventArgs e) {
            await LoadVpp(_calibNPointToNPointVppPath, _calibNPointToNPointVppPathTb);
        }

        // 标定和识别作业同时加载
        private async void tbBoth_item_Click(object sender, EventArgs e) {
            await LoadVpp(_calibrateVppPath, _calibrateTb);
            await LoadVpp(_calibNPointToNPointVppPath, _calibNPointToNPointVppPathTb);
        }

        #endregion
    }
}