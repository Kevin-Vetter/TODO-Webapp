using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public interface IRepo
    {
        #region ToDos
        void CreateToDo(string title, string description, Priority priority, int userId);
        void DeleteToDo(string guid);
        List<ToDo> GetAllToDosForUser(int UserID);
        ToDo GetToDoById(string id);
        void UpdateToDo(string guid, string description, Priority priority, bool completed);
        void CompleteToDo(string guid);
        void DeleteCompleted();
        #endregion


        #region User
        int LogIn(string username, string password);
        User GetUser(string username);
        void SignUp(string firstName, string lastName, string username, string password);
        #endregion
    }
}
