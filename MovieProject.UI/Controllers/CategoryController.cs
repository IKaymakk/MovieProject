using DTO.UI_Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MovieProject.UI.Controllers
{
    [Authorize]  // Kullanıcı giriş yapmış ve geçerli token'a sahip olmalı
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> CategoryList()
        {
           
            var client = _httpClientFactory.CreateClient();

            // Authorization header'ına token ekliyoruz

            // API'ye istek gönderiliyor
            var response = await client.GetAsync("https://localhost:44358/api/Genre");

            // API'den gelen yanıt işleniyor
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GenreDto>>(jsondata);
                return View(values);
            }

            return View(new List<GenreDto>());
        }
    }
}
