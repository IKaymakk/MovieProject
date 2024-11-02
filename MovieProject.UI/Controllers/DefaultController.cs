using Microsoft.AspNetCore.Mvc;

namespace MovieProject.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
