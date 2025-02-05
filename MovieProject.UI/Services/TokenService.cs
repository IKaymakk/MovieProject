namespace MovieProject.UI.Services;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetToken()
    {
        // Burada session'dan tokeni alıyoruz //
        return _httpContextAccessor.HttpContext.Session.GetString("AccessToken");
    }
}

