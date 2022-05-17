using TaskManager.Contexts;
using TaskManager.Data.Models;

namespace TaskManager.Data.Services;

public class TimeTaskService : ITaskService
{
    private ITaskContext? _context;
    private IEnumerable<BaseTask>? _tasks;

    public BaseTask? SelectedTask { get; set; }
    public TimeTaskService()
    {
        _context = new TimeTaskContext();
        _tasks = GetTasksAsync().Result.ToList();
        
        var newTask = new TimeTask();
        newTask.Duration = TimeSpan.FromMinutes(50);
        newTask.WorkPeriod = TimeSpan.FromMinutes(20);
        _context.CreateTaskAsync(newTask);
        _tasks = _context.GetTasksAsync().Result.ToArray();
    }
    public async Task<IEnumerable<BaseTask>> CreateTaskAsync(BaseTask task)
    {
        _tasks = _context.CreateTaskAsync(task).Result.ToArray();
        return await GetTasksAsync();
    }

    public async Task<IEnumerable<BaseTask>> GetTasksAsync()
    {
        return await _context.GetTasksAsync();
    }

    public async Task<BaseTask> GetTaskAsync(Guid id)
    {
        return await _context.GetTaskAsync(id);
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