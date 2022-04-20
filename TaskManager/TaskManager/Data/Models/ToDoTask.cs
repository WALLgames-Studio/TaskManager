namespace TaskManager.Data.Models;

public class ToDoTask : BaseTask
{
    public string Description { get; set; }
    public DateTimeOffset? Deadline { get; set; }

    public ToDoTask() : base()
    {
        Description = "";
        Deadline = null;
    }

    public ToDoTask(string title) : base(title)
    {
        Description = "";
        Deadline = null;
    }

    public ToDoTask(string title, string description) : base(title)
    {
        Description = description;
        Deadline = null;
    }
}