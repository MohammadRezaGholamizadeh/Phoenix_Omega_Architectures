using DataAccessLayer.EFTech.EFDataContexts;
using IdentityLayer.AspDotNetIdentity.Domain;
using Invio.Extensions.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InfrastructureLayer.Configurations.IdentityConfigurations.AspIdentities
{
    public static class AspDotnetIdentityConfiguration
    {
        public static IServiceCollection AddAspIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var aspIdentityConfig =
                configuration.GetAspIdentityConfig();
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                setup =>
                {
                    setup.Password.RequireNonAlphanumeric =
                            aspIdentityConfig.PasswordConfig
                                             .RequireNonAlphanumeric;

                    setup.Password.RequireLowercase =
                            aspIdentityConfig.PasswordConfig
                                             .RequireLowercase;

                    setup.Password.RequireUppercase =
                            aspIdentityConfig.PasswordConfig
                                             .RequireUppercase;

                    setup.Password.RequiredLength =
                            aspIdentityConfig.PasswordConfig
                                             .RequiredLength;

                    setup.Password.RequireDigit =
                            aspIdentityConfig.PasswordConfig
                                             .RequireDigit;

                    setup.Lockout.AllowedForNewUsers =
                            aspIdentityConfig.LockoutAllowedForNewUsers;
                }).AddEntityFrameworkStores<EFDataContext>()
                  .AddDefaultTokenProviders();

            var jwtSection =
                configuration
                .GetSection($"{nameof(AspIdentityConfig)}:{nameof(JwtBearerTokenSetting)}s");
            services.Configure<JwtBearerTokenSetting>(jwtSection);
            var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSetting>();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = aspIdentityConfig.JwtBearerTokenSettings.RequireHttpsMetadata;
                options.SaveToken = aspIdentityConfig.JwtBearerTokenSettings.SaveToken;
                options.Audience = aspIdentityConfig.JwtBearerTokenSettings.Audience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = aspIdentityConfig.JwtBearerTokenSettings.Audience,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.AddQueryStringAuthentication();
            });
            services.AddHttpContextAccessor();
            return services;
        }

        public static AspIdentityConfig GetAspIdentityConfig(
           this IConfiguration configuration)
        {
            var aspIdentityConfig = new AspIdentityConfig();
            configuration.Bind(nameof(AspIdentityConfig), aspIdentityConfig);
            return aspIdentityConfig;
        }
    }
}
