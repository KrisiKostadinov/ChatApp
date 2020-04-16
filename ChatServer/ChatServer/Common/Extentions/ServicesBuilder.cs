using AutoMapper;
using ChatServer.Data.Extentions;
using ChatServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatServer.Common.Extentions
{
    public static class ServicesBuilder
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<IUserService, UsersService>();

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
            => services.AddAutoMapper(typeof(AutoMapping));
    }
}
