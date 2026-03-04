namespace MlazAPIs.DTOs.Response
{
    public class UserResponse
    {
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;   
        public string UserEmail { get; set; } = null!;
        public string UserRoles { get; set; } = null!;
    }
}
