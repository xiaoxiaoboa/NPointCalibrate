using System;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1.Interface;

namespace WindowsFormsApp1.Views.Forms {
    public partial class MyRecordDisplay : UserControl, IMyControl {
        // 事件
        public event EventHandler MaximizeRestoreRequested;


        public MyRecordDisplay() {
            InitializeComponent();
        }

        public void SetRecord(ICogRecord record) {
            cogRecordsDisplay1.Subject = record;
        }

        public void SetGraphic(ICogImage image) { }

        public void SetLabelText(string text) {
            label1.Text = text;
        }

        public void Label_DoubleClick(object sender, EventArgs e) {
            MaximizeRestoreRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}