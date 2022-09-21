using TODO_Webapp.Model;

namespace TODO_Webapp.Service.Interface
{
    public class Repository : IRepo
    {
        private List<ToDo> _toDos = new List<ToDo>();

        /// <summary>
        /// Creates new todo
        /// </summary>
        /// <param name="description"></param>
        /// <param name="deadline"></param>
        public void CreateToDo(string description, DateTime? deadline)
        {
            _toDos.Add(new ToDo("",description, deadline));
        }

        /// <summary>
        /// Creates new todo
        /// </summary>
        /// <param name="description"></param>
        /// <param name="deadline"></param>
        /// <param name="priority"></param>
        public void CreateToDo(string description, DateTime? deadline, Priority priority)
        {
            _toDos.Add(new ToDo("", description, deadline, priority));
        }

        /// <summary>
        /// Get a todo by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>object of ToDo</returns>
        public ToDo GetToDoById(string id) 
        {
                return _toDos.First(t => t.Id == id);
        }

        /// <summary>
        /// Gets All ToDos
        /// </summary>
        /// <returns></returns>
        public List<ToDo> GetAllToDos() => _toDos.OrderBy(t => t.CreationPointInTime).ToList();

        /// <summary>
        /// Update an existing ToDo
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="description"></param>
        /// <param name="deadline"></param>
        /// <param name="priority"></param>
        /// <param name="completed"></param>
        public void UpdateToDo(string guid, string description, DateTime deadline, Priority priority, bool completed) {
            DeleteToDo(guid);
            _toDos.Add(new ToDo(guid ,description, deadline, completed, priority));
        }

        /// <summary>
        /// Delete a ToDo
        /// </summary>
        /// <param name="guid"></param>
        public void DeleteToDo(string guid)
        {
            _toDos.Remove(GetToDoById(guid));
        }

        /// <summary>
        /// Set a ToDo to completed
        /// </summary>
        /// <param name="guid"></param>
        public void CompleteToDo(string guid)
        {
            ToDo idk = GetToDoById(guid);
            DeleteToDo(guid);
            _toDos.Add(idk with { IsCompleted = true });
        }

        /// <summary>
        /// Removes all completed tasks
        /// </summary>
        public void DeleteCompleted()
        {
            _toDos.RemoveAll(t => t.IsCompleted);
        }
    }
}
