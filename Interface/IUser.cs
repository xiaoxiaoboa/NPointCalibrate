namespace WindowsFormsApp1.Interface {
    public interface IUser {
        string UserName{ get; set; }
        string Password{ get; set; }
        int Flag{ get; set; }
    }
}