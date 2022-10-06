using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public interface IRepo
    {
        #region ToDos
            void CreateToDo(string description, Priority priority);
            void DeleteToDo(string guid);
            List<ToDo> GetAllToDos();
            ToDo GetToDoById(string id);
            void UpdateToDo(string guid, string description, Priority priority, bool completed);
            void CompleteToDo(string guid);
            void DeleteCompleted();
        #endregion


        #region User
            int LogIn(string username, string password);
            User GetUser(string username);
        #endregion
    }
}
