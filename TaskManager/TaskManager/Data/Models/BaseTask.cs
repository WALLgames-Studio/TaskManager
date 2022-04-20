namespace TaskManager.Data.Models;

public class BaseTask
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsDone { get; set; }

    public BaseTask()
    {
        Id = Guid.NewGuid();
        Title = "Task :" + Id.ToString();
        IsDone = false;
    }

    public BaseTask(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsDone = false;
    }
}