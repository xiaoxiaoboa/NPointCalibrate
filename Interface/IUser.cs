namespace WindowsFormsApp1.Interface {
    public interface IUser {
        string UserName{ get; }
        string Password{ get;  }
        int Flag{ get; set; }
        int LastLogin{ get; set; }
    }
}