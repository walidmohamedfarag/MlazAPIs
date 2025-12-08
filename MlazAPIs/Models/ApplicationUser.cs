using Microsoft.AspNetCore.Identity;

namespace MlazAPIs.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FulltName { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
