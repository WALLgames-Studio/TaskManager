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
        _tasks = GetTasksAsync().Result.ToList();
    }
    public async Task<IEnumerable<BaseTask>> CreateTaskAsync(BaseTask task)
    {
        var newTask = task as ToDoTask;
        _tasks = _context.CreateTaskAsync(newTask).Result.ToArray();
        return _tasks;
    }

    public Task<IEnumerable<BaseTask>> GetTasksAsync()
    {
        return _context.GetTasksAsync();
    }

    public Task<BaseTask> GetTaskAsync(Guid id)
    {
        return _context.GetTaskAsync(id);
    }

    public Task<BaseTask> UpdateTaskAsync(Guid id, BaseTask updatedTask)
    {
        return _context.UpdateTaskAsync(id, updatedTask);
    }

    public void DeleteTask(Guid id)
    {
        _context.DeleteTask(id);
    }
}