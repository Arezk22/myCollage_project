using asp.netDay2.Models;
using asp.netDay2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.netDay2.Controllers
{
    public class CourseController : Controller
    {
       
            private readonly ItiContext itiContext;
            public CourseController(ItiContext context)
            {
                itiContext = context;
            }
        public IActionResult Index()
        {
            ICollection<Course> Courses = itiContext.Courses.Take(100).ToList();
            return View(Courses);
        }
        public IActionResult Details(int id)
        {
            var course = itiContext.Courses.Where(s => s.CrsId == id).Include(s => s.Top).FirstOrDefault();
            var Top = course.Top;
            CourseVM crsVM = new CourseVM()
            {
                CourseName = course.CrsName,
                CourseTop = Top.TopName,
                CrsDuration =course.CrsDuration,
            };
            itiContext.SaveChanges();
            return View(crsVM);
        }
        public IActionResult Create()
        {
            // return the create view to enter data
            return View();
        }
        [HttpPost]
        public IActionResult Save(Course crs)
        {
            if (ModelState.IsValid)
            {

                // create new instructor using context
                itiContext.Courses.Add(crs);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var currentCrs = itiContext.Courses.Where(c => c.CrsId == id).FirstOrDefault();
            return View(currentCrs);
        }
        [HttpPost]
        public IActionResult Edit(Course crs)
        {
            var currentcrs = itiContext.Courses.Where(c => c.CrsId == crs.CrsId).FirstOrDefault();
            currentcrs.CrsName = crs.CrsName;
            currentcrs.CrsDuration = crs.CrsDuration;
            currentcrs.TopId = crs.TopId;
            itiContext.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult delete(int id)
        {
            var currentCrs = itiContext.Courses.Where(c => c.CrsId == id).FirstOrDefault();
            if (currentCrs != null)
            {
                // get ins_courses for this instructor 
                //var topCourses = itiContext.Topics.Where(c => c.TopId == id).ToList();
                // remove ins_courses for this instructor
                //itiContext.Topics.RemoveRange(topCourses);
                itiContext.Courses.RemoveRange(currentCrs);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");

        }
    }
}
