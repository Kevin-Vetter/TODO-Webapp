namespace TODO_Webapp.Model
{
    public record ToDo
    {
        public string Id { get;} = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreationPointInTime { get; set; } = DateTime.Today;
        public DateTime Deadline { get; set; } 
        public Priority Importance { get; set; }

        public ToDo(string description, DateTime? deadline, Priority priority = Priority.Normal)
        {
            if (deadline == null)
                Deadline = DateTime.Today.AddDays(1);
            else
                Deadline = DateTime.Today.Date;
            Description = description;
            Importance = priority;
        }
    }
    public enum Priority
    {
        Low,
        Normal,
        High
    }
}
