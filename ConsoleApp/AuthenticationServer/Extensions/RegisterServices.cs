using AuthenticationServer.Interfaces;
using AuthenticationServer.Services;
using BL.Interfaces;
using BL.Services;
using Shared.Config;
using Shared.Extensions;
using Shared.Interfaces;

namespace AuthenticationServer.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthenticationService>();
            services.AddSingleton<IAuthServerConfig>(context =>
            {
                return new AuthServerConfig().GetAuthServerConfig();
            });

            return services;
        }
    }
}
