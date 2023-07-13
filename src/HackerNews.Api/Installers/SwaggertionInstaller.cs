using Microsoft.OpenApi.Models;

namespace HackerNews.Api.Installers
{
    public static class SwaggertionInstaller
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "This is Hacker News API",
                    Description = "An ASP.NET Core Web API for retrieving best stories from Hacker News. https://github.com/HackerNews/API",
                    Contact = new OpenApiContact
                    {
                        Name = "Yurii Maliuha",
                        Url = new Uri("https://www.linkedin.com/in/yurii-maliuha-a97868108/")
                    }
                });
            });

            return services;
        }
    }
}
