using DTO.UI_Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MovieProject.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _client;

        public UserController(IHttpClientFactory client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetUserDetails()
        {
            try
            {
                // Kullanıcının giriş yapıp yapmadığını kontrol et
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Json(new { success = false, message = "Kimlik doğrulama başarısız!" });
                }

                // UserId'yi integer'a çevir
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return Json(new { success = false, message = "Geçersiz kullanıcı kimliği!" });
                }

                // API isteği için HttpClient oluştur
                var client = _client.CreateClient();
                var response = await client.GetAsync($"https://localhost:44358/api/AppUser/GetUser?id={userId}");

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Kullanıcı bilgisi alınamadı!" });
                }

                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonSerializer.Deserialize<UserDetailsDto>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Json(new { success = true, data = values });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu!", error = ex.Message });
            }
        }

    }
}
