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
        // 用户数据文件路径
        private string _path = Path.Combine(Application.StartupPath, "data/users.csv");

        private static readonly UserData _instance = new UserData();

        public static UserData Instance => _instance;

        // 用户列表
        private List<IUser> _users = new List<IUser>();

        // 记住密码的用户
        private List<IUser> _flagUsers = new List<IUser>();
        public List<IUser> FlagUsers => _flagUsers;

        // 最后登录的用户
        public IUser LastLoginUser{ get; set; }


        private UserData() { }

        // 从文件获取数据
        /// <summary>
        /// 初始化用户数据。
        /// 从指定的CSV文件中读取用户信息，并将数据加载到内存中。
        /// 如果文件不存在，则抛出FileNotFoundException异常。
        /// 在读取过程中，会解析每一行数据，提取用户名、密码、记住密码标志以及最后登录标志。
        /// 记住密码的用户会被存储到_flagUsers列表中，最后登录的用户会被赋值给LastLoginUser属性。
        /// 所有用户信息会被存储到_users列表中，供后续操作使用。
        /// 如果在初始化过程中发生任何异常，将抛出包含具体错误信息的Exception异常。
        /// </summary>
        public void Initialize() {
            try
            {
                if (!File.Exists(_path))
                {
                    throw new FileNotFoundException("users.csv not found");
                }
                var lines = File.ReadAllLines(_path);
                for (int i = 1; i < lines.Length; i++) {
                    var columns = lines[i].Split(',');
                    var userName = columns[0];
                    var password = columns[1];
                    var flag = int.Parse(columns[2]);
                    var lastLogin = int.Parse(columns[3]);
                    // 记住密码的用户
                    if (flag == 1) {
                        _flagUsers.Add(new User(userName, password, flag, lastLogin));
                    }

                    // 最后一次登录的用户
                    if (lastLogin == 1) {
                        LastLoginUser = new User(userName, password, flag, lastLogin);
                    }

                    _users.Add(new User(userName, password, flag, lastLogin));
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
           
        }


        // 登录
        /// <summary>
        /// 执行用户登录操作。
        /// 该方法会根据传入的用户信息验证用户是否存在，并更新用户的登录状态及记住密码标志。
        /// 如果用户存在，会更新用户的最后登录状态，并将之前的最后登录用户状态重置。
        /// 同时，如果用户的记住密码标志发生变化，会同步更新该标志。
        /// 登录成功后，会调用SaveCsv方法保存更新后的用户数据到CSV文件。
        /// </summary>
        /// <param name="user">包含登录信息的用户对象，包括用户名、密码、记住密码标志等。</param>
        /// <returns>返回一个字符串，表示登录结果。如果用户不存在，则返回"用户不存在"；如果登录成功，则返回null。</returns>
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


        // 存储csv
        /// <summary>
        /// 将当前内存中的用户数据保存到CSV文件中。
        /// 该方法会以异步方式将_users列表中的所有用户信息写入到指定的CSV文件中。
        /// 文件内容包括用户的用户名、密码、记住密码标志以及最后登录标志，每行存储一个用户的数据。
        /// 如果文件写入过程中发生任何异常，不会直接捕获，需由调用方处理。
        /// </summary>
        /// <return>无返回值。</return>
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