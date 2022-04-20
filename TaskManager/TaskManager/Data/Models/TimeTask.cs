using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace TaskManager.Data.Models;

using TaskManagerLib;
public class TimeTask : BaseTask
{
    private int counterS = 0;
    private TimeSpan _duration;
    public TimerHandler TimeHandler { get; set; }

    public TimeSpan Duration
    {
        get => _duration;
        set
        {
            if (value.TotalMilliseconds <= 0)
            {
                _duration = TimeSpan.Zero;
                CurrentState = State.Done;
            }
            else
            {
                _duration = value;
            }
        }
    }

    public TimeSpan BreakPeriod { get; set; }
    public TimeSpan WorkPeriod { get; set; }
    public State CurrentState { get; set; }
    public int BreakSessions { get; set; }
    public int WorkSessions { get; set; }
    public TimeSpan CurrentTime { get; set; }

    public TimeTask() : base()
    {
        TimeHandler = new TimerHandler();
        Duration = TimeSpan.FromMinutes(1);
        BreakPeriod = TimeSpan.Zero;
        WorkPeriod = TimeSpan.Zero;
        CurrentState = State.WorkState;
        BreakSessions = 0;
        WorkSessions = 1;
        
        
    }
    
    public TimeTask(string title) : base(title)
    {
        TimeHandler = new TimerHandler();
        Id = Guid.NewGuid();
        Duration = TimeSpan.FromMinutes(1);
        BreakPeriod = TimeSpan.Zero;
        WorkPeriod = TimeSpan.Zero;
        CurrentState = State.WorkState;
        BreakSessions = 0;
        WorkSessions = 1;
    }
    
    public TimeTask(string title, double duration) : base(title)
    {
        TimeHandler = new TimerHandler();
        Id = Guid.NewGuid();
        Duration = TimeSpan.FromMinutes(duration);
        BreakPeriod = TimeSpan.Zero;
        WorkPeriod = TimeSpan.Zero;
        CurrentState = State.WorkState;
        BreakSessions = 0;
        WorkSessions = 1;
    }
    
    public TimeTask(string title, double duration, double workPeriod, double breakPeriod)
    : base(title)
    {
        TimeHandler = new TimerHandler();
        Id = Guid.NewGuid();
        Duration = TimeSpan.FromMinutes(duration);
        BreakPeriod = TimeSpan.FromMinutes(breakPeriod);
        WorkPeriod = TimeSpan.FromMinutes(workPeriod);
        CurrentState = State.WorkState;
        BreakSessions = 0;
        WorkSessions = 1;
    }

    public void SetUpTimeHandler()
    {
        TimeHandler.TimeCounter.Elapsed += OnTimerElapsed;
        TimeHandler.SetTime(
            (int)WorkPeriod.TotalHours,
            (int)WorkPeriod.TotalMinutes,
            (int)WorkPeriod.TotalSeconds);
        TimeHandler.Start();
    }
    
    public void SetTimeTaskConditions(
        int durationS,
        int workPeriodS,
        int breakPeriodS,
        int workSessions,
        int breakSessions)
    {
        Duration = TimeSpan.FromSeconds(durationS);
        WorkPeriod = TimeSpan.FromSeconds(workPeriodS);
        BreakPeriod = TimeSpan.FromSeconds(breakPeriodS);
        WorkSessions = workSessions;
        BreakSessions = breakSessions;

        CurrentTime = WorkPeriod;
        
        SetUpTimeHandler();
    }

    public override string ToString()
    {
        string ret = "Duration: " + Duration.TotalSeconds.ToString() +
                     " WorkPeriod: " + WorkPeriod.TotalSeconds.ToString() +
                     " BreakPeriod: " + BreakPeriod.TotalSeconds.ToString() +
                     "\nWorkSession: " + WorkSessions.ToString() +
                     " BreakSessions: " + BreakSessions.ToString() +
                     " CurrentTime: " + CurrentTime.TotalSeconds.ToString();

        return ret;
    }

    private void OnTimerElapsed(Object? sender, System.Timers.ElapsedEventArgs e)
    {
        Console.WriteLine(this.ToString());
        
        if (TimeHandler.TimeCounter != null)
        {
            if (WorkSessions == 0)
            {
                CurrentState = State.Done;
                TimeHandler.TimeCounter.Enabled = false;
                TimeHandler.TimeCounter.Dispose();
            }
            if (CurrentState == State.WorkState)
            {
                if (counterS >= (int) WorkPeriod.TotalSeconds)
                {
                    CurrentState = State.BreakState;
                    WorkSessions -= 1;
                    counterS = 0;
                    CurrentTime = BreakPeriod;
                }
                else
                {
                    counterS += 1;
                    CurrentTime = TimeSpan.FromSeconds((int)CurrentTime.TotalSeconds - 1);
                }
            }
            else
            {
                if (counterS >= (int) BreakPeriod.TotalSeconds)
                {
                    CurrentState = State.WorkState;
                    BreakSessions -= 1;
                    counterS = 0;
                    CurrentTime = WorkPeriod;
                }
                else
                {
                    counterS += 1;
                    CurrentTime = TimeSpan.FromSeconds((int)CurrentTime.TotalSeconds - 1);
                }
            }
        }
    }
}

public enum State
{
    WorkState,
    BreakState,
    Done
}