using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.DTOs.Requests
{
    public class ReportRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        public IFormFile? Image { get; set; } 
        [Required]
        public int Latitude { get; set; }
        [Required]
        public int Longitude { get; set; }

    }
}
