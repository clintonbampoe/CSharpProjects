namespace CodingTracker.Services;

class Validation
{
    public bool IsValidSessionDuration (DateTime start, DateTime end) => _isValidSessionDuration(start, end);

    private bool _isValidSessionDuration(DateTime startTime, DateTime endTime)
    {
        return (endTime > startTime);
    }
}