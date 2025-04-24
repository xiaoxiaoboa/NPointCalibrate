using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Interface;

namespace WindowsFormsApp1.Common.Login {
    public class UserData {
        private static readonly UserData _instance = new UserData();

        public static UserData Instance => _instance;

        private List<IUser> _users = new List<IUser>();
        private string _path = Path.Combine(Application.StartupPath, "data/users.csv");

        // 记住密码的用户
        public User flagUser;

        public static event Action<string> ErrorOccurred;
        public static event Action DataLoaded;

        private UserData() {
            Task.Run(() => {
                try {
                    var lines = File.ReadAllLines(_path);
                    for (int i = 1; i < lines.Length; i++) {
                        var columns = lines[i].Split(',');
                        var userName = columns[0];
                        var password = columns[1];
                        var flag = int.Parse(columns[2]);
                        if (flag == 1) {
                            flagUser = new User(userName, password, flag);
                        }

                        _users.Add(new User(userName, password, flag));

                        DataLoaded?.Invoke();
                    }
                }
                catch (Exception exception) {
                    ErrorOccurred?.Invoke("读取用户数据失败：" + exception.Message);
                }
            });
        }

        public string Login(IUser user) {
            var res = _users.FirstOrDefault(u => u.UserName == user.UserName);
            if (res != null) {
                if (res.Flag != user.Flag) {
                    res.Flag = res.Flag == 0 ? 0 : 1;
                    SaveCsv();
                }

                return null;
            }

            return "用户不存在";
        }


        private async void SaveCsv() {
            using (StreamWriter sw = new StreamWriter(_path, false, Encoding.UTF8)) {
                await sw.WriteLineAsync("username,password,flag");

                foreach (var u in _users) {
                    await sw.WriteLineAsync($"{u.UserName},{u.Password},{u.Flag}");
                }
            }
        }
    }
}