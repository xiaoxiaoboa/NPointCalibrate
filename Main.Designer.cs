using WindowsFormsApp1;
using WindowsFormsApp1.Views.Forms;

namespace WindowsFormsApp1 {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.indicatorLight1 = new WindowsFormsApp1.IndicatorLight();
            this.panel8 = new System.Windows.Forms.Panel();
            this.indicatorLight2 = new WindowsFormsApp1.IndicatorLight();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.initCamera = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.connectPlc = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.startLive = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.takePho = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.maxControl = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.myDisplay1 = new MyDisplay();
            this.panel6 = new System.Windows.Forms.Panel();
            this.myDisplay2 = new MyDisplay();
            this.panel7 = new System.Windows.Forms.Panel();
            this.myRecordDisplay1 = new MyRecordDisplay();
            this.panel10 = new System.Windows.Forms.Panel();
            this.myRecordDisplay2 = new MyRecordDisplay();
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.camera = new System.Windows.Forms.ToolStripMenuItem();
            this.initCameraMenu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectCamera_item = new System.Windows.Forms.ToolStripMenuItem();
            this.stopLive_item = new System.Windows.Forms.ToolStripMenuItem();
            this.plc = new System.Windows.Forms.ToolStripMenuItem();
            this.connectPlc_item = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectPlc_item = new System.Windows.Forms.ToolStripMenuItem();
            this.vppCalibrate = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolBlock_item = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrate_item = new System.Windows.Forms.ToolStripMenuItem();
            this.identification_item = new System.Windows.Forms.ToolStripMenuItem();
            this.tbBoth_item = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrateWork_item = new System.Windows.Forms.ToolStripMenuItem();
            this.identificationWork_item = new System.Windows.Forms.ToolStripMenuItem();
            this.runlog = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLog_item = new System.Windows.Forms.ToolStripMenuItem();
            this.operation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel11.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.miniToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.miniToolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1234, 657);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1234, 628);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel11, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(364, 622);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(358, 180);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.60378F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.54717F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.84906F));
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel8, 1, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(352, 57);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机状态：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLC状态：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.indicatorLight1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(202, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(20, 22);
            this.panel2.TabIndex = 2;
            // 
            // indicatorLight1
            // 
            this.indicatorLight1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.indicatorLight1.IsOn = false;
            this.indicatorLight1.Location = new System.Drawing.Point(0, -1);
            this.indicatorLight1.Name = "indicatorLight1";
            this.indicatorLight1.OffColor = System.Drawing.Color.Gray;
            this.indicatorLight1.OnColor = System.Drawing.Color.LimeGreen;
            this.indicatorLight1.Size = new System.Drawing.Size(20, 25);
            this.indicatorLight1.TabIndex = 0;
            this.indicatorLight1.Text = "indicatorLight1";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.indicatorLight2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(202, 31);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(3);
            this.panel8.Size = new System.Drawing.Size(20, 23);
            this.panel8.TabIndex = 3;
            // 
            // indicatorLight2
            // 
            this.indicatorLight2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.indicatorLight2.IsOn = false;
            this.indicatorLight2.Location = new System.Drawing.Point(0, -1);
            this.indicatorLight2.Name = "indicatorLight2";
            this.indicatorLight2.OffColor = System.Drawing.Color.Gray;
            this.indicatorLight2.OnColor = System.Drawing.Color.LimeGreen;
            this.indicatorLight2.Size = new System.Drawing.Size(20, 25);
            this.indicatorLight2.TabIndex = 0;
            this.indicatorLight2.Text = "indicatorLight2";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel9, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel12, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.panel13, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 66);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(352, 111);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.initCamera);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20, 0, 5, 0);
            this.panel3.Size = new System.Drawing.Size(170, 49);
            this.panel3.TabIndex = 0;
            // 
            // initCamera
            // 
            this.initCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.initCamera.Location = new System.Drawing.Point(20, 0);
            this.initCamera.Name = "initCamera";
            this.initCamera.Size = new System.Drawing.Size(145, 49);
            this.initCamera.TabIndex = 0;
            this.initCamera.Text = "初始化相机";
            this.initCamera.UseVisualStyleBackColor = true;
            this.initCamera.Click += new System.EventHandler(this.init_camera_btn_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.connectPlc);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(179, 3);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.panel9.Size = new System.Drawing.Size(170, 49);
            this.panel9.TabIndex = 1;
            // 
            // connectPlc
            // 
            this.connectPlc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectPlc.Location = new System.Drawing.Point(5, 0);
            this.connectPlc.Name = "connectPlc";
            this.connectPlc.Size = new System.Drawing.Size(145, 49);
            this.connectPlc.TabIndex = 0;
            this.connectPlc.Text = "连接PLC";
            this.connectPlc.UseVisualStyleBackColor = true;
            this.connectPlc.Click += new System.EventHandler(this.connect_plc_Click);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.startLive);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(3, 58);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(20, 0, 5, 0);
            this.panel12.Size = new System.Drawing.Size(170, 50);
            this.panel12.TabIndex = 2;
            // 
            // startLive
            // 
            this.startLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startLive.Enabled = false;
            this.startLive.Location = new System.Drawing.Point(20, 0);
            this.startLive.Name = "startLive";
            this.startLive.Size = new System.Drawing.Size(145, 50);
            this.startLive.TabIndex = 0;
            this.startLive.Text = "实时相机";
            this.startLive.UseVisualStyleBackColor = true;
            this.startLive.Click += new System.EventHandler(this.startLive_Click);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.takePho);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(179, 58);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.panel13.Size = new System.Drawing.Size(170, 50);
            this.panel13.TabIndex = 3;
            // 
            // takePho
            // 
            this.takePho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.takePho.Enabled = false;
            this.takePho.Location = new System.Drawing.Point(5, 0);
            this.takePho.Name = "takePho";
            this.takePho.Size = new System.Drawing.Size(145, 50);
            this.takePho.TabIndex = 0;
            this.takePho.Text = "拍照";
            this.takePho.UseVisualStyleBackColor = true;
            this.takePho.Click += new System.EventHandler(this.takePho_Click);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.Control;
            this.panel11.Controls.Add(this.listView1);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 189);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(358, 430);
            this.panel11.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.CausesValidation = false;
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(358, 430);
            this.listView1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listView1, "双击放大/缩小");
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.maxControl, this.exportCsv });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 52);
            // 
            // maxControl
            // 
            this.maxControl.Name = "maxControl";
            this.maxControl.Size = new System.Drawing.Size(137, 24);
            this.maxControl.Text = "放大";
            this.maxControl.Click += new System.EventHandler(this.maxControl_Click);
            // 
            // exportCsv
            // 
            this.exportCsv.Name = "exportCsv";
            this.exportCsv.Size = new System.Drawing.Size(137, 24);
            this.exportCsv.Text = "导出CSV";
            this.exportCsv.Click += new System.EventHandler(this.exportCsv_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.tableLayoutPanel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(373, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(858, 622);
            this.panel4.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel10, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(858, 622);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.myDisplay1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(423, 305);
            this.panel5.TabIndex = 0;
            // 
            // myDisplay1
            // 
            this.myDisplay1.BackColor = System.Drawing.SystemColors.Control;
            this.myDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDisplay1.Location = new System.Drawing.Point(0, 0);
            this.myDisplay1.Name = "myDisplay1";
            this.myDisplay1.Size = new System.Drawing.Size(423, 305);
            this.myDisplay1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.myDisplay2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(432, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(423, 305);
            this.panel6.TabIndex = 1;
            // 
            // myDisplay2
            // 
            this.myDisplay2.BackColor = System.Drawing.SystemColors.Control;
            this.myDisplay2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDisplay2.Location = new System.Drawing.Point(0, 0);
            this.myDisplay2.Name = "myDisplay2";
            this.myDisplay2.Size = new System.Drawing.Size(423, 305);
            this.myDisplay2.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.myRecordDisplay1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 314);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(423, 305);
            this.panel7.TabIndex = 2;
            // 
            // myRecordDisplay1
            // 
            this.myRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myRecordDisplay1.Location = new System.Drawing.Point(0, 0);
            this.myRecordDisplay1.Name = "myRecordDisplay1";
            this.myRecordDisplay1.Size = new System.Drawing.Size(423, 305);
            this.myRecordDisplay1.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.myRecordDisplay2);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(432, 314);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(423, 305);
            this.panel10.TabIndex = 3;
            // 
            // myRecordDisplay2
            // 
            this.myRecordDisplay2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myRecordDisplay2.Location = new System.Drawing.Point(0, 0);
            this.myRecordDisplay2.Name = "myRecordDisplay2";
            this.myRecordDisplay2.Size = new System.Drawing.Size(423, 305);
            this.myRecordDisplay2.TabIndex = 0;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.SystemColors.Window;
            this.miniToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.camera, this.plc, this.vppCalibrate, this.runlog, this.operation });
            this.miniToolStrip.Location = new System.Drawing.Point(0, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(1234, 29);
            this.miniToolStrip.TabIndex = 0;
            // 
            // camera
            // 
            this.camera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.initCameraMenu_item, this.disconnectCamera_item, this.stopLive_item });
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(51, 25);
            this.camera.Text = "相机";
            // 
            // initCameraMenu_item
            // 
            this.initCameraMenu_item.Name = "initCameraMenu_item";
            this.initCameraMenu_item.Size = new System.Drawing.Size(153, 24);
            this.initCameraMenu_item.Text = "初始化相机";
            this.initCameraMenu_item.Click += new System.EventHandler(this.initCamera_menuItem_Click);
            // 
            // disconnectCamera_item
            // 
            this.disconnectCamera_item.Enabled = false;
            this.disconnectCamera_item.Name = "disconnectCamera_item";
            this.disconnectCamera_item.Size = new System.Drawing.Size(153, 24);
            this.disconnectCamera_item.Text = "断开相机";
            this.disconnectCamera_item.Click += new System.EventHandler(this.disconnectCamera_item_Click);
            // 
            // stopLive_item
            // 
            this.stopLive_item.Enabled = false;
            this.stopLive_item.Name = "stopLive_item";
            this.stopLive_item.Size = new System.Drawing.Size(153, 24);
            this.stopLive_item.Text = "停止实时";
            this.stopLive_item.Click += new System.EventHandler(this.stopLive_Click);
            // 
            // plc
            // 
            this.plc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.connectPlc_item, this.disconnectPlc_item });
            this.plc.Name = "plc";
            this.plc.Size = new System.Drawing.Size(48, 25);
            this.plc.Text = "PLC";
            // 
            // connectPlc_item
            // 
            this.connectPlc_item.Name = "connectPlc_item";
            this.connectPlc_item.Size = new System.Drawing.Size(108, 24);
            this.connectPlc_item.Text = "连接";
            this.connectPlc_item.Click += new System.EventHandler(this.connect_plc_Click);
            // 
            // disconnectPlc_item
            // 
            this.disconnectPlc_item.Name = "disconnectPlc_item";
            this.disconnectPlc_item.Size = new System.Drawing.Size(108, 24);
            this.disconnectPlc_item.Text = "断开";
            this.disconnectPlc_item.Click += new System.EventHandler(this.disconnect_plc_Click);
            // 
            // vppCalibrate
            // 
            this.vppCalibrate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.loadToolBlock_item, this.calibrateWork_item, this.identificationWork_item });
            this.vppCalibrate.Name = "vppCalibrate";
            this.vppCalibrate.Size = new System.Drawing.Size(51, 25);
            this.vppCalibrate.Text = "作业";
            // 
            // loadToolBlock_item
            // 
            this.loadToolBlock_item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.calibrate_item, this.identification_item, this.tbBoth_item });
            this.loadToolBlock_item.Name = "loadToolBlock_item";
            this.loadToolBlock_item.Size = new System.Drawing.Size(138, 24);
            this.loadToolBlock_item.Text = "加载tb";
            // 
            // calibrate_item
            // 
            this.calibrate_item.Name = "calibrate_item";
            this.calibrate_item.Size = new System.Drawing.Size(138, 24);
            this.calibrate_item.Text = "标定";
            this.calibrate_item.Click += new System.EventHandler(this.calibrate_item_Click);
            // 
            // identification_item
            // 
            this.identification_item.Name = "identification_item";
            this.identification_item.Size = new System.Drawing.Size(138, 24);
            this.identification_item.Text = "识别";
            this.identification_item.Click += new System.EventHandler(this.identification_item_Click);
            // 
            // tbBoth_item
            // 
            this.tbBoth_item.Name = "tbBoth_item";
            this.tbBoth_item.Size = new System.Drawing.Size(138, 24);
            this.tbBoth_item.Text = "两项同时";
            this.tbBoth_item.Click += new System.EventHandler(this.tbBoth_item_Click);
            // 
            // calibrateWork_item
            // 
            this.calibrateWork_item.Name = "calibrateWork_item";
            this.calibrateWork_item.Size = new System.Drawing.Size(138, 24);
            this.calibrateWork_item.Text = "标定作业";
            this.calibrateWork_item.Click += new System.EventHandler(this.calibrate_Click);
            // 
            // identificationWork_item
            // 
            this.identificationWork_item.Name = "identificationWork_item";
            this.identificationWork_item.Size = new System.Drawing.Size(138, 24);
            this.identificationWork_item.Text = "识别作业";
            this.identificationWork_item.Click += new System.EventHandler(this.identificationWork_item_Click);
            // 
            // runlog
            // 
            this.runlog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.exportLog_item });
            this.runlog.Name = "runlog";
            this.runlog.Size = new System.Drawing.Size(81, 25);
            this.runlog.Text = "运行日志";
            // 
            // exportLog_item
            // 
            this.exportLog_item.Name = "exportLog_item";
            this.exportLog_item.Size = new System.Drawing.Size(138, 24);
            this.exportLog_item.Text = "导出日志";
            this.exportLog_item.Click += new System.EventHandler(this.exportLog_Click);
            // 
            // operation
            // 
            this.operation.Name = "operation";
            this.operation.Size = new System.Drawing.Size(51, 25);
            this.operation.Text = "操作";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1234, 657);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.miniToolStrip.ResumeLayout(false);
            this.miniToolStrip.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolStripMenuItem stopLive_item;

        private System.Windows.Forms.Button initCamera;
        private System.Windows.Forms.Button connectPlc;
        private System.Windows.Forms.Button startLive;
        private System.Windows.Forms.Button takePho;

        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;

        private System.Windows.Forms.Panel panel3;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;

        private System.Windows.Forms.ToolStripMenuItem connectPlc_item;
        private System.Windows.Forms.ToolStripMenuItem disconnectPlc_item;

        private WindowsFormsApp1.IndicatorLight indicatorLight1;
        private WindowsFormsApp1.IndicatorLight indicatorLight2;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;

        private MyRecordDisplay myRecordDisplay1;
        private MyRecordDisplay myRecordDisplay2;

        private System.Windows.Forms.Panel panel10;

        private System.Windows.Forms.ToolStripMenuItem calibrate_item;
        private System.Windows.Forms.ToolStripMenuItem identification_item;
        private System.Windows.Forms.ToolStripMenuItem tbBoth_item;

        private System.Windows.Forms.ToolStripMenuItem identificationWork_item;

        private System.Windows.Forms.ToolStripMenuItem loadToolBlock_item;

        private System.Windows.Forms.ToolStripMenuItem disconnectCamera_item;

        private System.Windows.Forms.ToolStripMenuItem initCameraMenu_item;

        private System.Windows.Forms.ToolStripMenuItem exportLog_item;

        private System.Windows.Forms.ToolStripMenuItem maxControl;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportCsv;

        private System.Windows.Forms.ToolTip toolTip1;

        private System.Windows.Forms.ListView listView1;

        private MyDisplay myDisplay1;
        private MyDisplay myDisplay2;

        private System.Windows.Forms.Panel panel11;

        private System.Windows.Forms.ToolStripMenuItem calibrateWork_item;

        private System.Windows.Forms.ToolStripMenuItem operation;

        private System.Windows.Forms.ToolStripMenuItem plc;

        private System.Windows.Forms.ToolStripMenuItem runlog;

        private System.Windows.Forms.ToolStripMenuItem camera;
        private System.Windows.Forms.ToolStripMenuItem vppCalibrate;

        


        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;

        private System.Windows.Forms.Panel panel4;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        private System.Windows.Forms.MenuStrip miniToolStrip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        private System.Windows.Forms.Panel panel1;

        #endregion
    }
}