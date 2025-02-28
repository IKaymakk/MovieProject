using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MovieProject.UI.Models;
using System.Security.Claims;
using DTO.UI_Dtos;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MovieProject.UI.Controllers;

public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    //[HttpGet]
    //public async Task<IActionResult> Login()
    //{
    //    return View();
    //}
    public async Task<JsonResult> Login(string username, string password)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("https://localhost:44358/api/AppUser/Login", new { username, password });

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre" });
        }

        var result = await response.Content.ReadFromJsonAsync<JwtResponse>();

        // **Token'ı çözümle**
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(result.Token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
        var userId = userIdClaim?.Value;

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, "User"),
        new Claim("AccessToken", result.Token),
        new Claim("UserId", userId) // **UserId'yi claims içine ekledik**
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Json(new
        {
            success = true,
            token = result.Token,
            username = username,
            role = "User",
            userId = userId,
            expiresAt = DateTimeOffset.UtcNow.AddHours(1)
        });
    }

    [HttpPost]
    public async Task<JsonResult> SignUp([FromBody] CreateAppUserDto dto)
    {
        dto.appRoleId = 1;
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Lütfen tüm alanları doldurun." });
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:44358/api/AppUser/CreateUser", dto);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Hata: {errorMessage}" });
            }

            return Json(new { success = true, message = "Kayıt Başarılı" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Çıkış yap
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Çerez ve session'ı temizle
        Response.Cookies.Delete("MovieProjectCookie", new CookieOptions
        {
            Path = "/",
            Secure = true,
            HttpOnly = true
        });
        HttpContext.Session.Clear();

        // Kullanıcıyı çıkış yaptıktan sonra yönlendir
        return RedirectToAction("Index", "Default");
    }


}
