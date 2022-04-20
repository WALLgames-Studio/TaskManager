using TaskManager.Data.Models;

namespace TaskManager.Contexts;

public class TodoTaskContext : ITaskContext
{
    private List<ToDoTask> _tasks = new List<ToDoTask>
    {
        new ToDoTask("T1"),
        new ToDoTask("t2")
    };

    public IEnumerable<BaseTask> CreateTask(BaseTask task)
    {
        ToDoTask newTask = task as ToDoTask;
        _tasks.Add(newTask);

        return _tasks;
    }

    public IEnumerable<BaseTask> GetTasks()
    {
        return _tasks;
    }

    public BaseTask GetTask(Guid id)
    {
        ToDoTask task = _tasks.FirstOrDefault(t => t.Id == id);
        return task;
    }

    public BaseTask UpdateTask(Guid id, BaseTask updatedTask)
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