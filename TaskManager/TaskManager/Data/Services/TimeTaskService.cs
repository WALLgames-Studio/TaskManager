using TaskManager.Contexts;
using TaskManager.Data.Models;

namespace TaskManager.Data.Services;

public class TimeTaskService : ITaskService
{
    private ITaskContext? _context;
    private IEnumerable<BaseTask>? _tasks;

    public TimeTaskService()
    {
        _context = new TimeTaskContext();
        _tasks = GetTasks().ToList();
        
        var newTask = new TimeTask();
        newTask.Duration = TimeSpan.FromMinutes(50);
        newTask.WorkPeriod = TimeSpan.FromMinutes(20);
        _context.CreateTask(newTask);
        _tasks = _context.GetTasks().ToArray();
    }
    public IEnumerable<BaseTask> CreateTask(BaseTask task)
    {
        _tasks = _context.CreateTask(task);
        return _tasks;
    }

    public IEnumerable<BaseTask> GetTasks()
    {
        return _context.GetTasks();
    }

    public BaseTask GetTask(Guid id)
    {
        return _context.GetTask(id);
    }

    public BaseTask UpdateTask(Guid id, BaseTask updatedTask)
    {
        return _context.UpdateTask(id, updatedTask);
    }

    public void DeleteTask(Guid id)
    {
        _context.DeleteTask(id);
    }
}