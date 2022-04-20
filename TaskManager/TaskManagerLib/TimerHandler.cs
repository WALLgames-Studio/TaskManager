namespace TaskManagerLib;
using System.Timers;

public class TimerHandler : ITimer
{
    public Timer TimeCounter { get; } = new Timer();
    private TimeSpan _duration = TimeSpan.Zero;

    public void Start()
    {
        if (_duration == TimeSpan.Zero) return;

        TimeCounter.Interval = 1000;
        TimeCounter.Enabled = true;
    }

    public void Stop()
    {
        TimeCounter.Enabled = false;
        TimeCounter.Dispose();
    }

    public void SetTime(int h, int m, int s)
    {
        _duration = new TimeSpan(h, m, s);
    }

    public (int h, int m, int s) GetCurrentTime()
    {
        return (_duration.Hours, _duration.Minutes, _duration.Seconds);
    }
}