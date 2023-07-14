namespace HackerNews.Application.Models;

public class HackerNewsConfiguration
{
    public string UrlBaseString { get; set; }

    private Uri _baseUri;
    public Uri UrlBase
    {
        get
        {
            if (_baseUri == null && UrlBaseString != null)
            {
                _baseUri = new Uri(UrlBaseString);
            }

            return _baseUri;
        }
    }
}
