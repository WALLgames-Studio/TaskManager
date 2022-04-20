using TaskManager.Data.Models;

namespace TaskManager.Data.Services;

public interface ITaskService
{
    public Task<IEnumerable<BaseTask>> CreateTaskAsync(BaseTask task);
    public Task<IEnumerable<BaseTask>> GetTasksAsync();
    public Task<BaseTask> GetTaskAsync(Guid id);
    public Task<BaseTask> UpdateTaskAsync(Guid id, BaseTask updatedTask);
    public void DeleteTask(Guid id);
}