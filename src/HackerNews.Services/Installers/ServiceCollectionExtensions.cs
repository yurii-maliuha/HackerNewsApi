using HackerNews.Application.Helpers;
using HackerNews.Application.Mappers;
using HackerNews.Application.Models;
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
            options.SizeLimit = 50;
        });

        services.Configure<HackerNewsConfiguration>(options =>
            configuration.GetSection(nameof(HackerNewsConfiguration)).Bind(options));

        services.AddSingleton<IStoryCacheReader, StoryCacheReader>();
        services.AddHttpClient<IStoryService, StoryService>();
        services.AddTransient<IStoryMapper, StoryMapper>();
        services.AddTransient<IDateTimeMapper, DateTimeMapper>();
        services.AddTransient<IHackerNewsService, HackerNewsService>();

        return services;
    }
}
