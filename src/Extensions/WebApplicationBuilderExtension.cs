using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder InitializeConfiguration(this WebApplicationBuilder builder)
    {
        builder.GetConfiguration();

        builder.ConfigureMvc();
        builder.ConfigureAuthetication();
        builder.ConfigureServices();

        return builder;
    }

    private static void GetConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey");
        Configuration.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
        Configuration.ApiKeyName = builder.Configuration.GetValue<string>("ApiKeyName");

        var smtp = new Configuration.SmtpConfiguration();
        builder.Configuration.GetSection("Smtp").Bind(smtp);
    }

    private static void ConfigureMvc(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
    }

    private static void ConfigureAuthetication(this WebApplicationBuilder builder)
    {
        var key = System.Text.Encoding.ASCII.GetBytes(Configuration.JwtKey);

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }

    private static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<BlogDataContext>();
        builder.Services.AddTransient<TokenService>();
    }

    public static WebApplication BuildApp(this WebApplicationBuilder builder)
        => builder.Build().InitializeConfiguration();
}