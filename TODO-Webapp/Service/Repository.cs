using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public class Repository : IRepo
    {
        private List<ToDo> _toDos = new List<ToDo>();

        public void CreateToDo(string description, DateTime? deadline)
        {
            _toDos.Add(new ToDo("",description, deadline));
        }
        public void CreateToDo(string description, DateTime? deadline, Priority priority)
        {
            _toDos.Add(new ToDo("", description, deadline, priority));
        }
        public ToDo GetToDoById(string id) 
        {
                return _toDos.First(t => t.Id == id);
        }
        public List<ToDo> GetAllToDos()
        {
            return _toDos;
        }
        public void UpdateToDo(string guid, string description, DateTime deadline, Priority priority, bool completed) {
            DeleteToDo(guid);
            _toDos.Add(new ToDo(guid ,description, deadline, completed, priority));
        }
        public void DeleteToDo(string guid)
        {
            _toDos.Remove(GetToDoById(guid));
        }
        public void CompleteToDo(string guid)
        {
            ToDo idk = GetToDoById(guid);
            DeleteToDo(guid);
            _toDos.Add(idk with { IsCompleted = true });
        }
    }
}
