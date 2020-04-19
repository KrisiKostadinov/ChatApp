using AutoMapper;
using ChatServer.Common.Mapping;
using ChatServer.Data.Extentions;
using ChatServer.Features.User.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChatServer.Common.Extentions
{
    public static class ServicesBuilder
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
            .AddScoped<IHaveCustomMappings, HaveCustomMappings>()
            .AddScoped<IUserService, UsersService>()
            .AddScoped<IFriendsService, FriendsService>();

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(typeof(HaveCustomMappings).GetTypeInfo().Assembly);

            return services;
        }
    }
}
