namespace TaskManagerLib;

public interface ITimer
{
    public void Start();
    public void Stop();
    public void SetTime(int h, int m, int s);
    public (int h, int m, int s) GetCurrentTime();
}