using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
