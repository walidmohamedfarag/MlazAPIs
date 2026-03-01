using System.ComponentModel.DataAnnotations;

namespace MlazAPIs.Models
{
    public enum ReportStatus
    {
        [Display(Name = "في الانتظار")]
        Pending = 1,
        [Display(Name = "تم الحل")]
        Resolved = 2,
        [Display(Name = "مرفوض")]
        Rejected = 3
    }
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImagePublicId { get; set; } = string.Empty;
        public string Location { get; set; } = null!;
        public ReportStatus Status { get; set; } = ReportStatus.Pending;
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}
