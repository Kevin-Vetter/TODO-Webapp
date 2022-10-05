namespace TODO_Webapp.Model
{
    public record ToDo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Today;
        public Priority Importance { get; set; }


        public ToDo(string? guid, string description, Priority priority = Priority.Normal)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                Id = guid;
            }
            Description = description;
            Importance = priority;
        }

        public ToDo(string? guid, string description, bool completed, Priority priority = Priority.Normal)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                Id = guid;
            }
            Description = description;
            Importance = priority;
            IsCompleted = completed;
        }
    }
    public enum Priority
    {
        Low,
        Normal,
        High
    }
}
