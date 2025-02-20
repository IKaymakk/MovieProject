using DTO.UI_Dtos;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            int.TryParse(userIdClaim, out int userId);
            dto.id = userId;

            try
            {
                var client = _client.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:44358/api/AppUser/ChangePassword", dto);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Parola Başarıyla Değiştirildi." });
                }

                // API'den gelen hataları al
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);

                if (jsonResponse.ContainsKey("errors")) // Eğer errors alanı varsa
                {
                    var errorMessages = jsonResponse["errors"]
                        .ToObject<Dictionary<string, List<string>>>();

                    var message = string.Join(", ", errorMessages.Values.SelectMany(v => v));
                    return Json(new { success = false, message });
                }
                else if (jsonResponse.ContainsKey("message")) // Eğer sadece message varsa
                {
                    return Json(new { success = false, message = jsonResponse["message"].ToString() });
                }
                else
                {
                    return Json(new { success = false, message = "Bilinmeyen bir hata oluştu." });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserFavoritedMovies(string? sortBy, int page = 1, int pageSize = 18)
        {
            // Kullanıcı ID'sini Claims'den al
            var userIdClaim = User.FindFirst("UserId")?.Value;
            int.TryParse(userIdClaim, out int userId);

            var client = _client.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/GetFavoritedMoviesByUserId?userId={userId}&sortBy={sortBy}&page={page}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var paginatedMovies = Newtonsoft.Json.JsonConvert.DeserializeObject<PaginatedMovieDto>(jsonData);

                return Json(new
                {
                    movies = paginatedMovies.Movies,
                    totalPages = paginatedMovies.TotalPages,
                    currentPage = page
                });
            }

            return BadRequest();
        }



    }
}
