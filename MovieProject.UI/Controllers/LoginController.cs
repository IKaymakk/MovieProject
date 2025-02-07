using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MovieProject.UI.Models;
using System.Security.Claims;
using DTO.UI_Dtos;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

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
    [HttpPost]
    public async Task<JsonResult> Login(string username, string password)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("https://localhost:44358/api/AppUser/Login", new { username, password });

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre" });
        }

        var result = await response.Content.ReadFromJsonAsync<JwtResponse>();

        // Token'ı Session'a kaydet
        HttpContext.Session.SetString("AccessToken", result.Token);

        // Kullanıcı bilgilerini al ve Claims oluştur
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

        // Cookie Authentication ile giriş yap
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
            expiresAt = DateTimeOffset.UtcNow.AddHours(1)
        });
    }
    [HttpPost]
    public async Task<JsonResult> SignUp(CreateAppUserDto dto)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("https://localhost:44358/api/AppUser/CreateUser", dto);
        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Hata!" });
        }
        return Json(new { success = true, message = "Kayıt Başarılı" });
    }
    [HttpPost]
    public IActionResult Logout()
    {
        // Token veya herhangi bir çerezi temizle
        Response.Cookies.Delete("AccessToken");

        // Diğer temizleme işlemleri (örneğin, oturumu sonlandırma)
        HttpContext.Session.Clear();

        // Kullanıcıyı çıkış yaptır
        return RedirectToAction("Index", "Home");
    }
}
