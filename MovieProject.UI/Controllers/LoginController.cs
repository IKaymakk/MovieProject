using Microsoft.AspNetCore.Mvc;

namespace MovieProject.UI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
