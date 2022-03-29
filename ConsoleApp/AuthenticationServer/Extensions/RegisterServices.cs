using AuthenticationServer.Interfaces;
using AuthenticationServer.Services;
using BL.Interfaces;
using BL.Services;

namespace AuthenticationServer.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
