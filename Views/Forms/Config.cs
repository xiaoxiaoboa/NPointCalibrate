using System;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1.Views.Forms {
    public partial class Config : Form {
        public Config() {
            InitializeComponent();

            try {
                var ip = IniControl.Instance.Read("PlcConfig", "IP");
                var port = IniControl.Instance.Read("PlcConfig", "Port");
                var baseX = IniControl.Instance.Read("PointConfig", "BaseX");
                var baseY = IniControl.Instance.Read("PointConfig", "BaseY");
                var baseAngle = IniControl.Instance.Read("PointConfig", "BaseAngle");
                var rotateX = IniControl.Instance.Read("PointConfig", "RotateCenterX");
                var rotateY = IniControl.Instance.Read("PointConfig", "RotateCenterY");

                textBox1.Text = baseX;
                textBox2.Text = ip;
                textBox3.Text = port;
                textBox4.Text = baseY;
                textBox5.Text = rotateX;
                textBox6.Text = baseAngle;
                textBox7.Text = rotateY;
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message);
            }
        }


        // 保存
        private void button1_Click(object sender, EventArgs e) {
            if (PlcControl.Instance.IsConnected) {
                Logger.Instance.AddLog("配置PLC IP前需要断开PLC连接");
                return;
            }

            var baseX = textBox1.Text;
            var ip = textBox2.Text;
            var port = textBox3.Text;
            var baseY = textBox4.Text;
            var rotateX = textBox5.Text;
            var baseAngle = textBox6.Text;
            var rotateY = textBox7.Text;

            IniControl.Instance.BaseX = Convert.ToSingle(baseX);
            IniControl.Instance.BaseY = Convert.ToSingle(baseY);
            IniControl.Instance.BaseAngle = Convert.ToSingle(baseAngle);
            IniControl.Instance.RotateCenterX = Convert.ToSingle(rotateX);
            IniControl.Instance.RotateCenterY = Convert.ToSingle(rotateY);
            IniControl.Instance.Ip = ip;
            IniControl.Instance.Port = Convert.ToInt32(port);

            try {
                IniControl.Instance.SaveFile();
                MessageBox.Show(@"保存成功");
            }
            catch (Exception exception) {
                Logger.Instance.AddLog(exception.Message);
            }
        }
    }
}