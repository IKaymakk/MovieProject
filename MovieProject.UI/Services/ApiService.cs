using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MovieProject.UI.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    public ApiService(HttpClient httpClient, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    // GET isteği
    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        try
        {
            var token = _tokenService.GetToken();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token not found.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetAsync(url);
        }
        catch (UnauthorizedAccessException)
        {
            // Token bulunamadığında, kullanıcıyı login sayfasına yönlendir
            throw new UnauthorizedAccessException("Token bulunamadı. Lütfen giriş yapın.");
        }
    }

    // POST isteği
    public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
    {
        try
        {
            var token = _tokenService.GetToken();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token not found.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync(url, content);
        }
        catch (UnauthorizedAccessException)
        {
            // Token bulunamadığında, kullanıcıyı login sayfasına yönlendir
            throw new UnauthorizedAccessException("Token bulunamadı. Lütfen giriş yapın.");
        }
    }

}
