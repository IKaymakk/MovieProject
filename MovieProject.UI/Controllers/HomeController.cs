using Microsoft.AspNetCore.Mvc;
using MovieProject.Application.Interfaces;
using MovieProject.UI.Models;
using MovieProject.UI.Services;
using System.Diagnostics;

namespace MovieProject.UI.Controllers;

public class HomeController:Controller
{
    private readonly ApiService _apiService;

    public HomeController(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _apiService.GetAsync("https://localhost:44358/api/AppUser/protected-data");

        if (!response.IsSuccessStatusCode)
        {
            return Unauthorized();
        }

        var data = await response.Content.ReadAsStringAsync();
        ViewBag.Data = data;

        return View();
    }
}
