using HackerNews.Services.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Services.Installers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IStoryMapper, StoryMapper>();
        services.AddTransient<IDateTimeMapper, DateTimeMapper>();
        services.AddHttpClient<IHackerNewsService, HackerNewsService>(client =>
        {
            client.BaseAddress = new Uri(configuration["HNBaseUrl"]);
        });

        return services;
    }
}
