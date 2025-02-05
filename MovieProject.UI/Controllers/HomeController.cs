using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Interfaces;
using MovieProject.UI.Models;
using System.Diagnostics;

namespace MovieProject.UI.Controllers;

public class HomeController : BaseController
{
    private readonly IApiClient _apiClient;

    public HomeController(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        if (string.IsNullOrEmpty(JwtToken))
            return RedirectToAction("SignIn", "Auth");

        var response = await _apiClient.GetAsync("api/protected-data", JwtToken);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            ViewBag.Data = data;
        }
        else
        {
            ViewBag.Error = "Yetkiniz yok veya API çaðrýsý baþarýsýz.";
        }

        return View();
    }
}
