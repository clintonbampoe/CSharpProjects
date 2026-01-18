namespace CodingTracker.Models;
class CodingSession
{
    public int Id { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan Duration { get; private set; }

    public CodingSession(int sessionId, DateTime startTime, DateTime endTime)
    {
        Id = sessionId;
        StartTime = startTime;
        EndTime = endTime;
        Duration = EndTime - StartTime;
    }
}