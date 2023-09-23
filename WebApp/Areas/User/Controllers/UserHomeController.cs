using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.User.Controllers
{
    [Area("User")]
    public class UserHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
