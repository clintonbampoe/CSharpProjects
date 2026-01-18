namespace CodingTracker.Services;

class Validation
{
    public static bool IsValidSessionDuration (DateTime start, DateTime end) => _isValidSessionDuration(start, end);

    private static bool _isValidSessionDuration(DateTime startTime, DateTime endTime)
    {
        return (endTime > startTime);
    }
}