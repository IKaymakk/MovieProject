using DTO.UI_Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject_Domain.Entities;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace MovieProject.UI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MovieList(string? SortBy, int Page = 1, int PageSize = 18)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByFilter?sortBy={SortBy}&page={Page}&pageSize={PageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var paginatedMovies = JsonConvert.DeserializeObject<PaginatedMovieDto>(jsondata);
                TempData["moviecount2"] = paginatedMovies.Movies.Count();
                return View(paginatedMovies);
            }

            // Başarısız durumda boş model döndür
            return View(new PaginatedMovieDto());
        }

        [HttpGet]
        public async Task<IActionResult> FilteredMovieList(string? SortBy, int Page = 1, int PageSize = 18)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByFilter?sortBy={SortBy}&page={Page}&pageSize={PageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var paginatedMovies = JsonConvert.DeserializeObject<PaginatedMovieDto>(jsonData);

                return PartialView("_FilteredMovieList", paginatedMovies);
            }

            return BadRequest();
        }

        public async Task<IActionResult> MovieDetails(int id)
        {
            ViewBag.MovieId = id;
            TempData["MovieId"] = id;
            HttpContext.Session.SetInt32("MovieId", id);
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MovieDetails?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<MovieDetailDto>(jsondata);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MovieListByGenre(int id, string? sortBy, int page = 1, int pageSize = 18)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByGenre?id={id}&sortBy={sortBy}&page={page}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<PaginatedMovieDto>(jsondata);
                ViewBag.moviecount = values.Movies.Count();
                ViewBag.categoryId = id; // id'yi tekrar View'e gönderin
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MovieListByGenreSortBy(int id, string? sortBy, int page = 1, int pageSize = 18)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByGenre?id={id}&sortBy={sortBy}&page={page}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<PaginatedMovieDto>(jsondata);
                return Json(values); // JSON verisi döndürülüyor
            }
            return BadRequest("Film listesi alınamadı.");
        }

        [HttpGet]
        public async Task<JsonResult> SearchMovies(int? categoryId, string? sortBy, string? searchTerm, int page = 1, int pageSize = 18)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"https://localhost:44358/api/Movie/search?categoryId={categoryId}&searchTerm={searchTerm}&sortBy={sortBy}&page={page}&pageSize={pageSize}";

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<PaginatedMovieDto>(jsondata);

                return Json(values);
            }
            return Json(new { success = false, message = "Hata!" });
        }

        [HttpPost]
        public async Task<JsonResult> AddFavoriteMovie()
        {
            int appUserId = Convert.ToInt16(User.FindFirst("UserId")?.Value);
            int? movieId = HttpContext.Session.GetInt32("MovieId");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:44358/api/FavoriteMovies/AddFavMovies", new { appUserId, movieId });

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Film favorilere eklendi" });
            }

            return Json(new { success = false, message =  responseContent });
        }

        [HttpPost]
        public async Task<JsonResult> CheckFavoriteStatus()
        {
            int appUserId = Convert.ToInt16(User.FindFirst("UserId")?.Value);
            var movieId = HttpContext.Session.GetInt32("MovieId");

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/FavoriteMovies/CheckFavoriteStatus?userId={appUserId}&movieId={movieId}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

                if (result.isFavorited == true)
                {
                    return Json(new { success = true, isFavorited = true });
                }
            }

            return Json(new { success = false, isFavorited = false });
        }


    }
}
