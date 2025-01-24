using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
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

                // PaginatedMovieDto modelini view'e gönder
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
                ViewBag.moviecount = values.Movies.Count();
                ViewBag.categoryId = id; // id'yi tekrar View'e gönderin
                return PartialView("_FilteredMovieList", values);
            }
            return View();
        }

    }
}
