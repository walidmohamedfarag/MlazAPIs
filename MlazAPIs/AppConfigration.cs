
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
            })
                .AddEntityFrameworkStores<DataAccess.ApplicationDBContext>()
                .AddDefaultTokenProviders();
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
                       ValidIssuer = "https://localhost:7139",
                       ValidAudience = "https://localhost:7139",
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ghkjjhkjkhjhjkjkjhjkhjhkkjkhjhnnmnmjkdsdsdsds"))
                   };
               });
            services.AddTransient<ITokenService, TokenService>();

        }
    }
}
