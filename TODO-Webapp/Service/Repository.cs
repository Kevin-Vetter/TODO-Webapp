using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public class Repository : IRepo
    {
        private List<ToDo> _toDos = new List<ToDo>();

        public void CreateToDo(string description, DateTime? deadline)
        {
            _toDos.Add(new ToDo(description, deadline));
        }
        public void CreateToDo(string description, DateTime? deadline, Priority priority)
        {
            _toDos.Add(new ToDo(description, deadline, priority));
        }
        public ToDo GetToDoById(string id)
        {
                return _toDos.First(t => t.Id == id);
        }
        public List<ToDo> GetAllToDos()
        {
            return _toDos;
        }
        public void UpdateToDo(ToDo toDo) { }
        public void DeleteToDo(string guid)
        {
            _toDos.Remove(_toDos.First(g => g.Id == guid));
            
        }
    }
}
