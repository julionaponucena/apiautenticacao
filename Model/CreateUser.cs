public record CreateUser{
    public string name{get;init;}
    public string username{get;init;}
    public string password {get;init;}
    public bool isAdmin {get;init;}
}