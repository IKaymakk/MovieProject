using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MovieProject.UI.ViewComponents
{
    public class _MovieDetailsActors2VC:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _MovieDetailsActors2VC(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44358/api/Actor/MovieActors?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MovieDetailActorDto>>(jsondata);
                return View(values);
            }
            return View();
        }
    }
}
