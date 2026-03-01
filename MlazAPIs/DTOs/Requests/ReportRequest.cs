using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.DTOs.Requests
{
    public class ReportRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public IFormFile? Image { get; set; } 
        [Required]
        public string Location { get; set; } = null!;

    }
}
