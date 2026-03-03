using MlazAPIs.Services.Image_Service;
using MlazAPIs.Utility.DBInitializer;
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
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IImageUpload imageUpload, IReposatory<Report> reportReposatory, UserManager<ApplicationUser> userManager, ILogger<ReportsController> logger)
        {
            _imageUpload = imageUpload;
            _reportReposatory = reportReposatory;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost("Report")]
        public async Task<IActionResult> Report(ReportRequest reportRequest)
        {
            try
            {

                var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
                var user = await _userManager.FindByIdAsync(userIdFromToken!);
                if (user is null)
                    return BadRequest(new { message = "User not found" });
                var report = new Report
                {
                    Title = reportRequest.Title,
                    Description = reportRequest.Description,
                    Location = reportRequest.Location,
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while submitting the report.");
                return StatusCode(500, new { message = "internal server error" });
            }
        }
        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var reports = await _reportReposatory.GetAllAsync(rep => rep.UserId == userIdFromToken);
            if (reports is null || !reports.Any())
                return NotFound(new { message = "No reports found for this user" });
            return Ok(reports);
        }
        [HttpPut("StatusUpdate")]
        [Authorize(Roles = $"{StaticRole.Admin}")]
        public async Task<IActionResult> StatusUpdate(UpdateStatus updateStatus)
        {
            var report = await _reportReposatory.GetOneAsync(rep => rep.Id == updateStatus.Id);
            if(report is null)
                return BadRequest(new { message = "Report not found" });
            if (updateStatus.Message is null && updateStatus.IsApproved is null)
                report.Status = "يتم التحقق من البلاغ";
            else if (updateStatus.IsApproved == true && updateStatus.Message is not null)
            {
                report.Status = "تم حل البلاغ";
                report.Message = updateStatus.Message;
            }
            else if (updateStatus.IsApproved == false && updateStatus.Message is not null)
            {
                report.Status = "تم رفض البلاغ";
                report.Message = updateStatus.Message;
            }
            _reportReposatory.Update(report);
            await _reportReposatory.CommitAsync();
            return Ok(new { message = "Report status updated successfully" });
        }
    }
}
