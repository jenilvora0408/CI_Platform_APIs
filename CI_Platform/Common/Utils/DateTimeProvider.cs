namespace Common.Utils;

public class DateTimeProvider
{
    public static DateTimeOffset GetCurrentDateTime()
    {
        return DateTimeOffset.UtcNow;
    }
}
