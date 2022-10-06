using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public interface IRepo
    {
        #region ToDos
        public void ClearList();
        public void LoadList(int id);
        void CreateToDo(string description, Priority priority, int userId, string party);
        void DeleteToDo(string guid);
        List<ToDo> GetAllToDosForUser(int UserID);
        ToDo GetToDoById(string id);
        void UpdateToDo(string guid, string description, Priority priority);
        void CompleteToDo(string guid);
        List<ToDo> GetLoadedToDos();
        void DeleteCompleted();
        #endregion


        #region User
        int LogIn(string username, string password);
        User GetUser(string username);
        void SignUp(string firstName, string lastName, string username, string password);
        #endregion
    }
}
