using Microsoft.AspNetCore.Mvc;

namespace asp.netDay2.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
