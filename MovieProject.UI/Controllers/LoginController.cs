using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MovieProject.UI.Models;
using System.Security.Claims;

namespace MovieProject.UI.Controllers;

public class LoginController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("https://localhost:44358/api/AppUser/Login", new { username, password });

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre";
            return View();
        }

        var result = await response.Content.ReadFromJsonAsync<JwtResponse>();

        // 1️⃣ Token'ı Session'a kaydet
        HttpContext.Session.SetString("AccessToken", result.Token);

        // 2️⃣ Kullanıcı bilgilerini al ve Claims oluştur
        var claims = new List<Claim>
        {
    new Claim(ClaimTypes.Name, username),
    new Claim(ClaimTypes.Role, "User"), // Rol ekleyebilirsin
    new Claim("AccessToken", result.Token) // Token'ı claim olarak saklamak isteyebilirsin
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Tarayıcı kapansa bile giriş devam etsin
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // 1 saat sonra cookie silinsin
        };

        // 3️⃣ Cookie Authentication ile giriş yap
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }

}
