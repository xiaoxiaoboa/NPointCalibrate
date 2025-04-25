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
        public List<IUser> Users => _users;
        private string _path = Path.Combine(Application.StartupPath, "data/users.csv");

        // 记住密码的用户
        private List<IUser> _flagUsers = new List<IUser>();
        public List<IUser> FlagUsers => _flagUsers;

        public IUser LastLoginUser{ get; set; }


        private UserData() { }

        public void Initialize() {
            var lines = File.ReadAllLines(_path);
            for (int i = 1; i < lines.Length; i++) {
                var columns = lines[i].Split(',');
                var userName = columns[0];
                var password = columns[1];
                var flag = int.Parse(columns[2]);
                var lastLogin = int.Parse(columns[3]);
                // 记住密码的用户
                if (flag == 1) {
                    _flagUsers.Add(new User(userName, password, flag));
                }

                // 最后一次登录的用户
                if (lastLogin == 1) {
                    LastLoginUser = new User(userName, password, flag);
                }

                _users.Add(new User(userName, password, flag));
            }
        }


        public async Task<string> Login(IUser user) {
            var res = _users.FirstOrDefault(u => u.UserName == user.UserName);
            if (res != null) {
                if (res.Flag != user.Flag) {
                    res.Flag = user.Flag == 0 ? 0 : 1;
                }

                if (LastLoginUser != null || user.UserName != LastLoginUser?.UserName) {
                    var previousLastLogin = _users.FirstOrDefault(u => u.UserName == LastLoginUser?.UserName);
                    if (previousLastLogin != null) {
                        previousLastLogin.LastLogin = 0;
                    }

                    res.LastLogin = 1;
                }

                await SaveCsv();
                return null;
            }

            return "用户不存在";
        }


        private async Task SaveCsv() {
            using (StreamWriter sw = new StreamWriter(_path, false, Encoding.UTF8)) {
                await sw.WriteLineAsync("username,password,flag,lastlogin");

                foreach (var u in _users) {
                    await sw.WriteLineAsync($"{u.UserName},{u.Password},{u.Flag},{u.LastLogin}");
                }
            }
        }
    }
}