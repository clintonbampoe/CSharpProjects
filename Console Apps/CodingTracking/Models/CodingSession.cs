class CodingSession
{
    public int SessionId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan Duration { get; private set; }

    public CodingSession(int sessionId, DateTime startTime, DateTime endTime)
    {
        SessionId = sessionId;
        StartTime = startTime;
        EndTime = endTime;
        Duration = EndTime - StartTime;
    }
}