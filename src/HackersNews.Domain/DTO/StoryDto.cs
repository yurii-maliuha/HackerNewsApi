namespace HackerNews.Domain.DTO;

public record StoryDto
{
    public string Title { get; private set; }
    public string? Url { get; private set; }
    public DateTime Time { get; private set; }
    public int Score { get; private set; }
    public string PostedBy { get; private set; }
    public int CommentCount { get; private set; }

    public StoryDto(string title, DateTime time, int score, string postedBy, int commentCount, string? url)
    {
        Title = title;
        Url = url;
        Time = time;
        Score = score;
        PostedBy = postedBy;
        CommentCount = commentCount;
    }
}
