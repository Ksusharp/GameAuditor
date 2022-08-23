using Microsoft.AspNetCore.Mvc;

namespace GameAuditor.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
