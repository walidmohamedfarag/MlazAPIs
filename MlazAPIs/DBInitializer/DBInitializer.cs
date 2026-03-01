using MlazAPIs.DataAccess;
using System.Threading.Tasks;

namespace MlazAPIs.Utility.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDBContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<DBInitializer> logger;

        public DBInitializer(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, ApplicationDBContext _dbContext, ILogger<DBInitializer> _logger)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            dbContext = _dbContext;
            logger = _logger;
        }
        public void Initialize()
        {
            try
            {

                if (dbContext.Database.GetPendingMigrations().Any())
                    dbContext.Database.Migrate();
                if (!roleManager.Roles.Any())
                {
                    roleManager.CreateAsync(new(StaticRole.SuperAdmin)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new(StaticRole.Admin)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new(StaticRole.Employee)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new(StaticRole.User)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new(StaticRole.Gust)).GetAwaiter().GetResult();
                }
                userManager.CreateAsync(new ApplicationUser
                {
                    FullName = "SuperAdmin",
                    Email = "superadmin@gmail.com",
                    UserName = "superadmin@gmail.com",
                }, "SuperAdmin123").GetAwaiter().GetResult();

                var user = userManager.FindByEmailAsync("superadmin@gmail.com").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user!, StaticRole.SuperAdmin).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
        }
    }
}
