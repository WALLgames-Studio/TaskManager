using TaskManager.Contexts;
using TaskManager.Data.Models;

namespace TaskManager.Data.Services;

public class ToDoTaskService :ITaskService
{
    private ITaskContext? _context;
    private IEnumerable<BaseTask>? _tasks;

    public ToDoTaskService()
    {
        _context = new TodoTaskContext();
        _tasks = GetTasks().ToList();
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