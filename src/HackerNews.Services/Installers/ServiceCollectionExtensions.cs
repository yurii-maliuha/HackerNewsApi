using HackerNews.Application.Helpers;
using HackerNews.Application.Mappers;
using HackerNews.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Application.Installers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 90;
        });
        services.AddHttpClient<IStoryService, StoryService>(client =>
        {
            client.BaseAddress = new Uri(configuration["HNBaseUrl"]);
        });

        services.AddTransient<IStoryMapper, StoryMapper>();
        services.AddTransient<IDateTimeMapper, DateTimeMapper>();
        services.AddSingleton<IStoryCacheReader, StoryCacheReader>();
        services.AddTransient<IHackerNewsService, HackerNewsService>();

        return services;
    }
}
