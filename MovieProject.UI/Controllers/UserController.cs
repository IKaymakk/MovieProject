using DTO.UI_Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
                int.TryParse(userIdClaim, out int userId);

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

        [HttpPost]
        public async Task<JsonResult> UpdateUserDetails([FromBody] UpdateUserDetailsDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            int.TryParse(userIdClaim, out int userId);
            dto.id = userId;

            if (dto == null)
            {
                return Json(new { success = false, message = "Gönderilen veri boş!" });
            }

            try
            {
                var client = _client.CreateClient();
                var jsonContent = JsonSerializer.Serialize(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44358/api/AppUser/UpdateUser", content);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Bilgileriniz Güncellendi" });
                }
                else
                {
                    return Json(new { success = false, message = "Hata! Bilgilerinizi Kontrol Ediniz." });
                }
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu!", error = ex.Message });

            }
        }
    }
}
