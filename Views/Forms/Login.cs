using System;
using System.Windows.Forms;
using WindowsFormsApp1.Common.Login;

namespace WindowsFormsApp1.Views.Forms {
    public partial class Login : Form {
        public Login() {
            InitializeComponent();

            UserData.ErrorOccurred += UserDataOnErrorOccurred;
            UserData.DataLoaded += InstanceOnDataLoaded;
        }

        // 用户列表加载完触发事件
        private void InstanceOnDataLoaded() {
            var user = UserData.Instance?.flagUser;
            if (user != null) {
                textBox1.Text = user.UserName;
                textBox2.Text = user.Password;
                checkBox1.Checked = user.Flag == 1;
            }
        }

        // 加载数据失败触发事件
        private void UserDataOnErrorOccurred(string msg) {
            MessageBox.Show(msg);
        }

        // 登录按钮
        private void button1_Click(object sender, EventArgs e) {
            var message = UserData.Instance.Login(new User(textBox1.Text, textBox2.Text, checkBox1.Checked ? 1 : 0));
            if (message != null) {
                MessageBox.Show(message);
            }
        }

        // 重置输入
        private void button2_Click(object sender, EventArgs e) {
            textBox1.Text = "";
            textBox2.Text = "";
            checkBox1.Checked = false;
        }
    }
}