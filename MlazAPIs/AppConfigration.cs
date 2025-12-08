
using MlazAPIs.Utility.DBInitializer;

namespace MlazAPIs
{
    public static class AppConfigration
    {
        public static void Configure(this IServiceCollection services, string connectionString)
        {
            // configure database connection using the connection string
            services.AddDbContext<DataAccess.ApplicationDBContext>(options =>
                options.UseSqlServer(connectionString));
            // configure identity services
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = false;
                option.Password.RequireUppercase = false;
                option.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<DataAccess.ApplicationDBContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:7131",
                        ValidAudience = "https://localhost:7131",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ghkjjhkjkhjhjkjkjhjkhjhkkjkhjhnnmnmjkdsdsdsds"))
                    };
                });
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddCors(option =>
            {
                option.AddPolicy("AllowLocalhost", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:4200",
                        "http://localhost:5173",
                        "http://localhost:8080"
                        );
                });
            });
        }


    }
}
