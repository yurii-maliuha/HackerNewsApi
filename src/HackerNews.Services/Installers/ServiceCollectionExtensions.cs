using HackerNews.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Application.Installers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
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
