using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROJECTNAME.Core.Posts;
using PROJECTNAME.Infrastructure.JsonPlaceholderApi;

namespace PROJECTNAME.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<JsonPlaceholderClient>("JsonPlaceholderClient", config =>
            {
                config.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                config.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddTransient<IPostsApi, PostsApi>();
            
            return services;
        }
    }
}