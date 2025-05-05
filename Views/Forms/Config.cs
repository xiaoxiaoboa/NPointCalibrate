using System;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1.Views.Forms {
    public partial class Config : Form {
        public Config() {
            InitializeComponent();
         

            var ip = IniControl.Instance.Read("PLC", "IP");
            var port = IniControl.Instance.Read("PLC", "Port");


            textBox1.Text = ip;
            textBox2.Text = port;
        }


        private void button1_Click(object sender, EventArgs e) {
            if (PlcControl.Instance.IsConnected) {
                Logger.Instance.AddLog("配置PLC IP前需要断开PLC连接");
                return;
            }

            var ip = textBox1.Text;
            var port = textBox2.Text;

            IniControl.Instance.Write("PLC", "IP", ip);
            IniControl.Instance.Write("PLC", "Port", port);
        }
    }
}