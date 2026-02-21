using MlazAPIs.Services.Image_Service;
using System.Threading.Tasks;

namespace MlazAPIs.Areas.User.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("User")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IImageUpload _imageUpload;
        private readonly IReposatory<Report> _reportReposatory;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportsController(IImageUpload imageUpload, IReposatory<Report> reportReposatory, UserManager<ApplicationUser> userManager)
        {
            _imageUpload = imageUpload;
            _reportReposatory = reportReposatory;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Report(ReportRequest reportRequest)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var user = await _userManager.FindByIdAsync(userIdFromToken!);
            if (user is null)
                return BadRequest(new { message = "User not found" });
            var report = new Report
            {
                Title = reportRequest.Title,
                Latitude = reportRequest.Latitude,
                Longitude = reportRequest.Longitude,
                UserId = user!.Id,
            };
            if (reportRequest.Image is not null && reportRequest.Image.Length > 0)
            {
                var uploadResult = await _imageUpload.ImageUploadAsync(reportRequest.Image, "Mallaz");
                report.ImageUrl = uploadResult.Url;
                report.ImagePublicId = uploadResult.PublicId;
            }
            await _reportReposatory.AddAsync(report);
            await _reportReposatory.CommitAsync();
            return Ok(new { message = "Report submitted successfully" });
        }
        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier)!.Value;
            var reports = await _reportReposatory.GetAllAsync(rep => rep.UserId == userIdFromToken);
            if(reports is null || !reports.Any())
                return NotFound(new { message = "No reports found for this user" });
            return Ok(reports);
        }
    }
}
