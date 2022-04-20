using TaskManager.Data.Models;

namespace TaskManager.Contexts;

public interface ITaskContext
{
    public IEnumerable<BaseTask> CreateTask(BaseTask task);
    public IEnumerable<BaseTask> GetTasks();
    public BaseTask GetTask(Guid id);
    public BaseTask UpdateTask(Guid id, BaseTask updatedTask);
    public void DeleteTask(Guid id);
}