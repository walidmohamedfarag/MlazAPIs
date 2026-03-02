using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImagePublicId { get; set; } = string.Empty;
        public string Location { get; set; } = null!;
        public string Status { get; set; } = "قيد الانتظار";
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}
