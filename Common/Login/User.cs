using WindowsFormsApp1.Interface;

namespace WindowsFormsApp1.Common.Login {
    public class User : IUser {
        public string UserName{ get; set; }
        public string Password{ get; set; }
        public int Flag{ get; set; }

        public User(string userName, string password, int flag) {
            UserName = userName;
            Password = password;
            Flag = flag;
        }
    }
}