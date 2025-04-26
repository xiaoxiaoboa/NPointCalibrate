using System;
using System.Windows.Forms;
using Cognex.VisionPro;
using WindowsFormsApp1.Interface;

namespace WindowsFormsApp1.Views.Forms {
    public partial class MyRecordDisplay : UserControl, IMyControl {
        // 事件
        public event EventHandler MaximizeRestoreRequested;


        public MyRecordDisplay() {
            InitializeComponent();
        }

        public void SetRecord(ICogRecord record) {
            cogRecordDisplay1.Record = record;
        }
        public void SetGraphic(ICogImage image) {
            cogRecordDisplay1.Image = image;
            cogRecordDisplay1.AutoFit = true;
        }

        public void SetLabelText(string text) {
            label1.Text = text;
        }

        public void Label_DoubleClick(object sender, EventArgs e) {
            MaximizeRestoreRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}