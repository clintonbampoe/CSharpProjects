namespace CodingTracker.Models;
class CodingSession
{
    public long Id { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public TimeSpan Duration { get; }

    public CodingSession(long id)
    {
        Id = id;
    }

    public CodingSession(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = EndTime - StartTime;
    }

    public CodingSession(long id, DateTime startTime, DateTime endTime)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        Duration = EndTime - StartTime;
    }

    public CodingSession(long id, DateTime startTime, DateTime endTime,  TimeSpan duration)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
    }
}