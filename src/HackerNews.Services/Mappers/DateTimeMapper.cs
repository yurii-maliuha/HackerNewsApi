namespace HackerNews.Application.Mappers;

public class DateTimeMapper : IDateTimeMapper
{
    public DateTime MapFromUnixStamp(long unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
    }
}
