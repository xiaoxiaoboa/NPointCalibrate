using System;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1.Views.Forms {
    public partial class Config : Form {
        public Config() {
            InitializeComponent();


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


            IniControl.Instance.Write("PlcConfig", "IP", ip);
            IniControl.Instance.Write("PlcConfig", "Port", port);
            IniControl.Instance.Write("PointConfig", "BaseX", baseX);
            IniControl.Instance.Write("PointConfig", "BaseY", baseY);
            IniControl.Instance.Write("PointConfig", "RotateCenterX", rotateX);
            IniControl.Instance.Write("PointConfig", "BaseAngle", baseAngle);
            IniControl.Instance.Write("PointConfig", "RotateCenterY", rotateY);

            MessageBox.Show(@"保存成功");
        }
    }
}