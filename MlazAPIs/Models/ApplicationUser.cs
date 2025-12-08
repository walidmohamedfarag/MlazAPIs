using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlazAPIs.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(200)")]
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
