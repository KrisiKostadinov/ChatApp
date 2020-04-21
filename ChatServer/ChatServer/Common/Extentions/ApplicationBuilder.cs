using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ChatServer.Data.Extentions
{
    public static class ApplicationBuilder
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ChatContext>();

            dbContext.Database.Migrate();
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "My ChatApp API",
                        Version = "v1"
                    });
            });

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
            => app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My ChatApp API");
                    options.RoutePrefix = string.Empty;
                });
    }
}
