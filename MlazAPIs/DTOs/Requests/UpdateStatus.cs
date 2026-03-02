namespace MlazAPIs.DTOs.Requests
{
    public class UpdateStatus
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public bool? IsApproved { get; set; }
    }
}
