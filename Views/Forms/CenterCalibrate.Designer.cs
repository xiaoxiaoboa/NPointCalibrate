using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Views.Forms {
    partial class CenterCalibrate {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CenterCalibrate));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.operate = new System.Windows.Forms.ToolStripMenuItem();
            this.listen_item = new System.Windows.Forms.ToolStripMenuItem();
            this.result_item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.operate });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // operate
            // 
            this.operate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.listen_item, this.result_item });
            this.operate.Name = "operate";
            this.operate.Size = new System.Drawing.Size(51, 24);
            this.operate.Text = "操作";
            // 
            // listen_item
            // 
            this.listen_item.Name = "listen_item";
            this.listen_item.Size = new System.Drawing.Size(138, 24);
            this.listen_item.Text = "监听";
            this.listen_item.Click += new System.EventHandler(this.listen_item_Click);
            // 
            // result_item
            // 
            this.result_item.Enabled = false;
            this.result_item.Name = "result_item";
            this.result_item.Size = new System.Drawing.Size(138, 24);
            this.result_item.Text = "查看结果";
            this.result_item.Click += new System.EventHandler(this.result_item_Click);
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(0, 28);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(800, 422);
            this.cogRecordDisplay1.TabIndex = 1;
            // 
            // CenterCalibrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cogRecordDisplay1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CenterCalibrate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CenterCalibrate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CenterCalibrate_FormClosing);
            this.Load += new System.EventHandler(this.CenterCalibrate_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem listen_item;

        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;

        private System.Windows.Forms.ToolStripMenuItem result_item;
        private System.Windows.Forms.ToolTip toolTip1;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem operate;

        #endregion
    }
}