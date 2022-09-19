using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public interface IRepo
    {
        void CreateToDo(string description, DateTime? deadline);
        void CreateToDo(string description, DateTime? deadline, Priority priority);
        void DeleteToDo(string guid);
        List<ToDo> GetAllToDos();
        ToDo GetToDoById(string id);
        void UpdateToDo(string guid, string description, DateTime deadline, Priority priority);
    }
}
