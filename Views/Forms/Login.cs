using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Common.Login;
using WindowsFormsApp1.Interface;

namespace WindowsFormsApp1.Views.Forms {
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e) {
            InitUserData();
        }

        // 初始化数据
        public void InitUserData() {
            Task.Run(() => {
                try {
                    UserData.Instance.Initialize();

                    Invoke((MethodInvoker)delegate {
                        comboBox1.DataSource = UserData.Instance.FlagUsers;
                        comboBox1.DisplayMember = "UserName";

                        // 获取上次登录的用户
                        var lastLoginUser = UserData.Instance.LastLoginUser;
                        if (lastLoginUser != null) {
                            comboBox1.SelectedItem = lastLoginUser;
                            // 如果没有记住密码，就只给用户名
                            if (lastLoginUser.Flag == 1) {
                                textBox2.Text = lastLoginUser.Password;
                                checkBox1.Checked = lastLoginUser.Flag == 1;
                            }
                            else {
                                comboBox1.Text = lastLoginUser.UserName;
                            }
                        }
                    });
                }
                catch (Exception exception) {
                    MessageBox.Show($@"获取用户列表失败：{exception.Message}");
                }
            });
        }


        // 登录按钮
        private async void button1_Click(object sender, EventArgs e) {
            var loginUser = new User(comboBox1.Text, textBox2.Text, checkBox1.Checked ? 1 : 0);
            var message = await UserData.Instance.Login(loginUser);
            if (message != null) {
                MessageBox.Show(message);
            }
            else {
                var main = new Main();
                main.OnMainFormClose += (o, args) => { Close(); };
                Hide();
                main.Show();
            }
        }

        // 重置输入
        private void button2_Click(object sender, EventArgs e) {
            // textBox1.Text = "";
            textBox2.Text = "";
            checkBox1.Checked = false;
        }

        #region 鼠标hover密码框

        private void textBox2_MouseHover(object sender, EventArgs e) {
            textBox2.UseSystemPasswordChar = false;
        }

        private void textBox2_MouseLeave(object sender, EventArgs e) {
            textBox2.UseSystemPasswordChar = true;
        }

        #endregion

        // combox中选择时触发事件
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem is IUser selectUser) {
                textBox2.Text = selectUser.Password;
                checkBox1.Checked = selectUser.Flag == 1;
            }
        }
    }
}