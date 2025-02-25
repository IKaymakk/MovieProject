using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MovieProject.UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _http;

        public CommentController(IHttpClientFactory http)
        {
            _http = http;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByMovieId(int id)
        {
            var client = _http.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Comment/GetCommentsByMovieId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<CommentListDto>>(jsonData);
                return Ok(values);
            }
            return BadRequest("Bilinmeyen Bir Hata Oluştu!..");
        }
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDto dto)
        {
            if (dto == null)
            {
                return Json(new { success = false, message = "Gönderilen veri boş!" });
            }

            if (string.IsNullOrWhiteSpace(dto.commentTitle) || string.IsNullOrWhiteSpace(dto.commentText))
            {
                return Json(new { success = false, message = "Başlık ve yorum metni zorunludur!" });
            }

            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Kullanıcı kimliği bulunamadı." });
            }
            dto.appUserId = Convert.ToInt16(userId);

            if (!TempData.ContainsKey("MovieId"))
            {
                return Json(new { success = false, message = "Film kimliği bulunamadı." });
            }

            var filmId = TempData["MovieId"];
            dto.movieId = Convert.ToInt16(filmId);

            try
            {
                var client = _http.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:44358/api/Comment/CreateComment", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Hata! " + errorResponse });
                }

                return Json(new { success = true, message = "Yorumunuz Eklendi" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "İstek gönderilirken hata oluştu: " + ex.Message });
            }
        }


    }
}
