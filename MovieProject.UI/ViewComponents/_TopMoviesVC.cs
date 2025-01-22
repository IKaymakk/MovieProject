using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MovieProject.UI.ViewComponents;

public class _TopMoviesVC : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _TopMoviesVC(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44358/api/MovieGenre/Top24Movies");
        if (response.IsSuccessStatusCode)
        {
            var jsondata = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Movie2Dto>>(jsondata);
            return View(values);
        }
        return View();
    }
}
