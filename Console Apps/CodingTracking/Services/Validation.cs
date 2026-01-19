using CodingTracker.Models;

namespace CodingTracker.Services;

class Validation
{
    public static bool IsValidSession (CodingSession s) => _isValidSessionDuration(s);

    private static bool _isValidSessionDuration(CodingSession session)
    {
        return (session.EndTime - session.StartTime).TotalSeconds > 0;
    }
}