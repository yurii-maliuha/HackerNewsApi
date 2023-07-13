namespace HackerNews.Application.Mappers
{
    public interface IDateTimeMapper
    {
        public DateTime MapFromUnixStamp(long unixTimeStamp);
    }
}
