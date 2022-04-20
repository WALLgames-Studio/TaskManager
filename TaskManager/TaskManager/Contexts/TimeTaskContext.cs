using TaskManager.Data.Models;

namespace TaskManager.Contexts;

public class TimeTaskContext : ITaskContext
{
    private List<TimeTask> _tasks = new List<TimeTask>
    {
        new TimeTask("timer1"),
        new TimeTask("timer2"),
        new TimeTask("timer3")
    };

    public IEnumerable<BaseTask> CreateTask(BaseTask task)
    {
        TimeTask? newTask = task as TimeTask;
        
        if (newTask != null)
        {
            Console.WriteLine("New Task \n" + newTask.ToString());
            SetupTimerTask(newTask);
            Console.WriteLine("New Task After Setup: \n" + newTask.ToString());
            _tasks.Add(newTask);
        }

        return _tasks;
    }

    public IEnumerable<BaseTask> GetTasks()
    {
        return _tasks;
    }

    public BaseTask GetTask(Guid id)
    {
        TimeTask? task = _tasks.FirstOrDefault(t => t.Id == id);

        return task;
    }

    public BaseTask UpdateTask(Guid id, BaseTask updatedTask)
    {
        TimeTask newTask = _tasks.FirstOrDefault(t => t.Id == id);

        if (newTask == null)
        {
            return null;
        }

        newTask.Title = updatedTask.Title;
        newTask.IsDone = updatedTask.IsDone;
        newTask.Duration = (updatedTask as TimeTask).Duration;
        newTask.BreakPeriod = (updatedTask as TimeTask).BreakPeriod;
        newTask.WorkPeriod = (updatedTask as TimeTask).WorkPeriod;
        newTask.BreakSessions = (updatedTask as TimeTask).BreakSessions;
        newTask.WorkSessions = (updatedTask as TimeTask).WorkSessions;
        newTask.CurrentState = (updatedTask as TimeTask).CurrentState;

        return newTask;
    }

    public void DeleteTask(Guid id)
    {
        TimeTask toDelete = _tasks.FirstOrDefault(t => t.Id == id);

        if (toDelete != null)
        {
            _tasks.Remove(toDelete);
        }
    }
    
    private void SetupTimerTask(TimeTask? newTask)
    {
        if (newTask == null) return;
        
        if (newTask.WorkPeriod == TimeSpan.Zero && newTask.BreakPeriod == TimeSpan.Zero)
        {
            newTask.SetTimeTaskConditions(
                (int)newTask.Duration.TotalSeconds,
                (int)newTask.Duration.TotalSeconds,
                0,
                1,
                0);
        }
        else if (newTask.BreakPeriod == TimeSpan.Zero)
        {
            
            if (newTask.Duration == newTask.WorkPeriod)
            {
                newTask.SetTimeTaskConditions(
                    (int)newTask.Duration.TotalSeconds,
                    (int)newTask.Duration.TotalSeconds,
                    0,
                    1,
                    0);
            }
            else
            {
                int durS = (int)newTask.Duration.TotalSeconds;
                int wPS = (int)newTask.WorkPeriod.TotalSeconds;
                int bPS = durS % wPS;
                int wS = durS / wPS;
                int bS = wS - 1;

                Console.WriteLine($"durS: {0}", durS);
                Console.WriteLine($"wPS: {0}, bPS: {1}, wSessions: {2}, bSessions: {3}",
                    wPS, bPS, wS, bS);
                newTask.SetTimeTaskConditions(durS, wPS, bPS, wS, bS);
            }
        }
        // else if (newTask.WorkPeriod == TimeSpan.Zero)
        // {
        //     
        // }
    }


}