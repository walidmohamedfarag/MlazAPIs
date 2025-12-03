
using System.Threading.Tasks;

namespace MlazAPIs.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;

        public AccountsController(UserManager<ApplicationUser> _userManager, ITokenService _tokenService)
        {
            userManager = _userManager;
            tokenService = _tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var ifEmailExsite = await userManager.FindByEmailAsync(registerRequest.Email);
            if (ifEmailExsite is not null)
                return BadRequest(new
                {
                    Error = $"{registerRequest.Email} is already exsite"
                });
            var user = new ApplicationUser
            {
                UserName = $"{registerRequest.FirstName}{registerRequest.LastName}",
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
            };
            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(new
            {
                Success = "Regisrter Successfully"
            });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
                return BadRequest(new
                {
                    error = "Invalid email"
                });
            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPasswordValid)
                return BadRequest(new
                {
                    error = "Invalid password"
                });
            return Ok(new
            {
                success = "Login Successfully",
            });
        }
        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound();
            return Ok(new
            {
                success = "valid email",
                userId = user.Id
            });
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await userManager.FindByIdAsync(resetPassword.UserId);
            var token = await userManager.GeneratePasswordResetTokenAsync(user!);
            var result = await userManager.ResetPasswordAsync(user!, token, resetPassword.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(new
            {
                success = "Password Reset Successfully"
            });
        }
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(TokenApiRequest tokenApiRequest)
        {
            if (tokenApiRequest is null || tokenApiRequest.RefreshToken is null || tokenApiRequest.AccessToken is null)
                return BadRequest(new { error = "invalid client request" });
            var claims = tokenService.ExtractClimFromToken(tokenApiRequest.AccessToken);
            var userName = claims.Identity!.Name;
            var user = userManager.Users.FirstOrDefault(u => u.UserName == userName);
            if (user is null || user.RefreshToken is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest(new { error = "invalid client request" });
            var accessToken = tokenService.GetAccessToken(claims.Claims);
            var refreshToken = tokenService.GetRefreshToken();
            user.RefreshToken = refreshToken;
            await userManager.UpdateAsync(user);
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        [HttpPost("GustLogin")]
        public async Task<IActionResult> GustLogin()
        {
            var user = new ApplicationUser
            {
                FirstName = "Gust",
                LastName = $"User{Random.Shared.Next(1,9999)}",
            };
            user.UserName = $"{user.FirstName}{user.LastName}";
            await userManager.CreateAsync(user);  
            return Ok(new
            {
                success = "Login Successfully as Gust",
            });
        }
        [HttpPost, Authorize]
        [Route("Revok")]
        public async Task<IActionResult> Revok()
        {
            var userName = User.Identity.Name;
            var user = userManager.Users.FirstOrDefault(u => u.UserName == userName);
            if (user is null) return BadRequest(new { error = "user not found" });
            user.RefreshToken = null;
            await userManager.UpdateAsync(user);
            return NoContent();
        }


    }
}
