namespace MlazAPIs.Services.Token_Service
{
    public interface ITokenService
    {
        string GetAccessToken(IEnumerable<Claim> claims);
        string GetRefreshToken();
        ClaimsPrincipal ExtractClimFromToken(string token);
    }
}
