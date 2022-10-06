using System;
using TODO_Webapp.DAL;
using TODO_Webapp.Model;


namespace TODO_Webapp.Service.Interface
{
    public class Repository : IRepo
    {
        private DataAccess _dataAccess = new();
        private List<ToDo> _toDos = new List<ToDo>();

        public void LoadList(int id)
        {
            if (_toDos.Count == 0)
            {
                _toDos = GetAllToDosForUser(id);
            }

        }
        public void ClearList()
        {
            _toDos.Clear();
        }

        public List<ToDo> GetLoadedToDos() => _toDos;


        #region ToDo
        /// <summary>
        /// Creates new todo
        /// </summary>
        /// <param name="description"></param>
        /// <param name="created"></param>
        /// <param name="priority"></param>
        public void CreateToDo(string description, Priority priority, int userId, string party)
        {
            _toDos.Add(_dataAccess.CreateToDo(Guid.NewGuid(), description, priority, userId, party));
        }
        /// <summary>
        /// Get a todo by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>object of ToDo</returns>
        public ToDo GetToDoById(string id) => _toDos.First(t => t.Id == id);

        /// <summary>
        /// Gets All ToDos
        /// </summary>
        /// <returns></returns>
        public List<ToDo> GetAllToDosForUser(int UserID) => _dataAccess.GetUsersToDo(UserID);

        /// <summary>
        /// Update an existing ToDo
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="description"></param>
        /// <param name="created"></param>
        /// <param name="priority"></param>
        /// <param name="completed"></param>
        public void UpdateToDo(string guid, string description, Priority priority)
        {
            ToDo toDo = _toDos.First(x => x.Id == guid);
            toDo.Description = description;
            toDo.Importance = priority;
            _dataAccess.UpdateToDo(toDo);
        }

        /// <summary>
        /// Delete a ToDo
        /// </summary>
        /// <param name="guid"></param>
        public void DeleteToDo(string guid) => _dataAccess.DisableToDo(guid);

        /// <summary>
        /// Set a ToDo to completed
        /// </summary>
        /// <param name="guid"></param>
        public void CompleteToDo(string guid) => _dataAccess.CompleteToDo(guid);
        public void DeleteCompleted() => _toDos.ForEach(t => { if (t.IsCompleted) DeleteToDo(t.Id); });
        #endregion

        public User GetUser(string username) => _dataAccess.GetUser(username);

        public int LogIn(string username, string password)
        {
            User user = GetUser(username);
            if (password == user.Password)
                return user.ID;
            else
                return 0;
        }

        public void SignUp(string firstName, string lastName, string username, string password)
        {
            _dataAccess.SignUp(firstName, lastName, username, password);
        }
    }
}
