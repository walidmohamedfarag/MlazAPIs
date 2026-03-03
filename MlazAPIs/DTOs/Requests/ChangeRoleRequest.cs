using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.DTOs.Requests
{
    public class ChangeRoleRequest
    {
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }
}
