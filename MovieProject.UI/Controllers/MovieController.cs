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


        public async Task<IActionResult> MovieList(string? SortBy)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByFilter?sortBy={SortBy}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MovieDto>>(jsondata);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FilteredMovieList(string? SortyBy)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByFilter?sortBy={SortyBy}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MovieDto>>(jsonData);

                return PartialView("_FilteredMovieList", values); 
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
        public async Task<IActionResult> MovieListByGenre(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Movie/MoviesByGenre?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MovieListByGenreDto>>(jsondata);
                ViewBag.moviecount = values.Count();
                return View(values);
            }
            return View();
        }
    }
}
