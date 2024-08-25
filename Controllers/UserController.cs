using Microsoft.AspNetCore.Mvc;
using asp.netDay2.Models;

namespace asp.netDay2.Controllers
{
    public class UserController : Controller
    {
        private readonly ItiContext itiContext;
        public UserController(ItiContext context)
        {
            itiContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // create new user
                itiContext.Add(user);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
