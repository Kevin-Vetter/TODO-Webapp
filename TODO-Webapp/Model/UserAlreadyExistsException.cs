namespace TODO_Webapp.Model;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string s) : base(s) { }

}
