using System.Security.Cryptography;

namespace MlazAPIs.Services
{
    public class TokenService : ITokenService
    {
        public string GetAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hshjashjhashjashjhjahjhjashjhashdffddffdf"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7131",
                audience: "https://localhost:7131",
                claims: claims,
                signingCredentials: cred
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }


        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal ExtractClimFromToken(string token)
        {
            var tokenValidate = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hshjashjhashjashjhjahjhjashjhashdffddffdf"))
            };
            var tokenHandeller = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var claims = tokenHandeller.ValidateToken(token, tokenValidate, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");
            return claims;
        }
    }
}
