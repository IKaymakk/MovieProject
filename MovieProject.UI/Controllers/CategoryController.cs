using Microsoft.AspNetCore.Mvc;

namespace MovieProject.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
