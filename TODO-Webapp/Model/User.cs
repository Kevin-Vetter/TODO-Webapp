namespace TODO_Webapp.Model;
public record User
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public User() { }
    public User(int id, string name, string username, string password)
    {
        ID = id;
        FirstName = name;
        LastName = name;
        Username = username;
        Password = password;
    }
}