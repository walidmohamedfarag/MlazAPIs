using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.DTOs.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

    }
}
