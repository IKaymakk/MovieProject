using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MovieProject.UI.ViewComponents
{
    public class _MovieListVC : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _MovieListVC(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44358/api/Movie");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MovieDto>>(jsondata);
                TempData["moviecount2"] = values.Count();
                return View(values);
            }
            return View();
        }
    }
}
