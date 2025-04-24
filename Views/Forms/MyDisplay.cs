using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Cognex.VisionPro;
using WindowsFormsApp1.Interface;

namespace WindowsFormsApp1.Views.Forms {
    public partial class MyDisplay : UserControl, IMyControl {
        // 事件
        public event EventHandler MaximizeRestoreRequested;

        public MyDisplay() {
            InitializeComponent();
        }

        public void StartLive(object acqFifo, [Optional] bool own) {
            cogDisplay1.AutoFit = true;
            cogDisplay1.StartLiveDisplay(acqFifo);
        }

        public void StopLive() {
            cogDisplay1.StopLiveDisplay();

            cogDisplay1.Image = null;
        }

        public void SetGraphic(ICogImage image) {
            cogDisplay1.AutoFit = true;
            cogDisplay1.Image = image;
        }

        public bool LiveDisplayStatus() {
            return cogDisplay1.LiveDisplayRunning;
        }


        public void SetLabelText(string text) {
            label1.Text = text;
        }

        public void Label_DoubleClick(object sender, EventArgs e) {
            MaximizeRestoreRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}