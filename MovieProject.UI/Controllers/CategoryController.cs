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
        private readonly IApiClient _apiClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(IHttpClientFactory httpClientFactory, IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _apiClient = apiClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> CategoryList()
        {
            // Token'ı session'dan alıyoruz
            var jwtToken = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(jwtToken)) // Token boşsa giriş sayfasına yönlendir.
                return RedirectToAction("SignIn", "Auth");

            // API isteği için client oluşturuluyor
            var client = _httpClientFactory.CreateClient();

            // Authorization header'ına token ekliyoruz
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            // API'ye istek gönderiliyor
            var response = await client.GetAsync("https://localhost:44358/api/Genre");

            // API'den gelen yanıt işleniyor
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GenreDto>>(jsondata);
                return View(values);
            }

            // Hata durumunda boş bir liste döndürülüyor
            return View(new List<GenreDto>());
        }
    }
}
