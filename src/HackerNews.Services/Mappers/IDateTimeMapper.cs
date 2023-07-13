namespace HackerNews.Services.Mappers
{
    public interface IDateTimeMapper
    {
        public DateTime MapFromUnixStamp(long unixTimeStamp);
    }
}
