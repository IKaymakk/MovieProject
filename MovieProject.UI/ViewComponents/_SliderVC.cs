using DTO.UI_Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace MovieProject.UI.ViewComponents
{
    public class _SliderVC : ViewComponent
    {
        private readonly IHttpClientFactory _client;

        public _SliderVC(IHttpClientFactory client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var clientx = _client.CreateClient();
            var responsemessage = await clientx.GetAsync("https://localhost:44358/api/MovieGenre");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsondata = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<MovieGenreDto>>(jsondata);
                return View(values);
            }
            return View();
        }
    }
}
