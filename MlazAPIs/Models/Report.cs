using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImagePublicId { get; set; } = string.Empty;
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}
