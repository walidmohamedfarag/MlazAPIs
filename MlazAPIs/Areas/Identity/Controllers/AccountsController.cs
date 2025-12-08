using MlazAPIs.Utility.DBInitializer;
using System.Text.RegularExpressions;

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
                FullName = registerRequest.FullName.TrimEnd().TrimStart(),
                PhoneNumber = registerRequest.PhoneNumber,
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
            };
            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await userManager.AddToRoleAsync(user, StaticRole.User);
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
            var roles = await userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Role , string.Join(" ," , roles)),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };
            var accessToken = tokenService.GetAccessToken(claims);
            var role = string.Join(" ,", roles.ToList());
            return Ok(new
            {
                success = "Login Successfully",
                name = user.FullName,
                id = user.Id,
                userEmail = user.Email,
                userPhone = user.PhoneNumber,
                Role = role,
                token = accessToken,
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
        [HttpPost("GustLogin")]
        public async Task<IActionResult> GustLogin()
        {
            var user = new ApplicationUser
            {
                FullName = $"فاعل خير{Random.Shared.Next(1, 999)}",
            };
            user.UserName = user.FullName;
            await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, StaticRole.Gust);
            var roles = string.Join(" ,", await userManager.GetRolesAsync(user));
            var claims = new[]
            {
                new Claim(ClaimTypes.Name , user.FullName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Role , roles),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            };
            var accessToken = tokenService.GetAccessToken(claims);
            return Ok(new
            {
                success = "Login Successfully as فاعل خير",
                name = user.FullName.Substring(0, 8),
                id = user.Id,
                role = roles,
                token = accessToken
            });
        }

    }
}
