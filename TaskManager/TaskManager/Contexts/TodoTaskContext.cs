using TaskManager.Data.Models;

namespace TaskManager.Contexts;

public class TodoTaskContext : ITaskContext
{
    private List<ToDoTask> _tasks = new List<ToDoTask>
    {
        new ToDoTask("T1"),
        new ToDoTask("t2")
    };

    public async Task<IEnumerable<BaseTask>> CreateTaskAsync(BaseTask task)
    {
        ToDoTask newTask = task as ToDoTask;
        _tasks.Add(newTask);

        return await GetTasksAsync();
    }

    public async Task<IEnumerable<BaseTask>> GetTasksAsync()
    {
        return _tasks;
    }

    public async Task<BaseTask> GetTaskAsync(Guid id)
    {
        ToDoTask task = _tasks.FirstOrDefault(t => t.Id == id);
        return task;
    }

    public async Task<BaseTask> UpdateTaskAsync(Guid id, BaseTask updatedTask)
    {
        ToDoTask newTask = _tasks.FirstOrDefault(t => t.Id == id);

        if (newTask == null)
        {
            return null;
        }

        newTask.Title = updatedTask.Title;
        newTask.IsDone = updatedTask.IsDone;
        newTask.Description = (updatedTask as ToDoTask).Description;
        newTask.Deadline = (updatedTask as ToDoTask).Deadline;

        return newTask;
    }

    public void DeleteTask(Guid id)
    {
        ToDoTask toDelete = _tasks.FirstOrDefault(t => t.Id == id);

        if (toDelete != null)
        {
            _tasks.Remove(toDelete);
        }
    }
}