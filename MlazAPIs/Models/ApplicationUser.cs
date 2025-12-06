using Microsoft.AspNetCore.Identity;

namespace MlazAPIs.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FulltName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime {  get; set; }

    }
}
