namespace HackerNews.Application.Mappers;

public interface IDateTimeMapper
{
    DateTime MapFromUnixStamp(long unixTimeStamp);
}
