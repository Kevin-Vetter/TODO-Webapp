namespace TODO_Webapp.Model;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string s) : base(s) { }
}
