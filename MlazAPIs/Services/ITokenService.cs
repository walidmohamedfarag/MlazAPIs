namespace MlazAPIs.Services
{
    public interface ITokenService
    {
        string GetAccessToken(IEnumerable<Claim> claims);
        string GetRefreshToken();
        ClaimsPrincipal ExtractClimFromToken(string token);
    }
}
