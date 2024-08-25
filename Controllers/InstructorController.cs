using asp.netDay2.Models;
using asp.netDay2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace asp.netDay2.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ItiContext itiContext;
        public InstructorController(ItiContext context)
        {
            itiContext = context;
        }
        
        public IActionResult Index()
        {
            ICollection<Instructor> Instructors = itiContext.Instructors.Take(100).ToList();
            return View(Instructors);
        }
        public IActionResult Details(int id)
        {
            var instructor = itiContext.Instructors.Where(s => s.InsId == id).Include(s => s.Dept).FirstOrDefault();
            var department = instructor.Dept;
            InstructorVM insVM = new InstructorVM()
            {
                InstructorName = instructor.InsName,
                InstructorDepartment = department.DeptName,
            }; 
            itiContext.SaveChanges();
            return View(insVM);
        }
        public IActionResult Create()
        {
            // return the create view to enter data
            return View();
        }
        [HttpPost]
        public IActionResult Save(Instructor ins)
        {
            if (ModelState.IsValid)
            {
                
                // create new instructor using context
                itiContext.Instructors.Add(ins);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var currentIns = itiContext.Instructors.Where(i =>i.InsId == id).FirstOrDefault();            
            return View(currentIns);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            var currentIns = itiContext.Instructors.Where(i => i.InsId == instructor.InsId).FirstOrDefault();
            currentIns.InsName = instructor.InsName;
            currentIns.InsDegree = instructor.InsDegree;
            currentIns.Salary = instructor.Salary;            
            itiContext.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult delete(int id) 
        {
            var currentIns = itiContext.Instructors.Where(i => i.InsId == id).FirstOrDefault();
            if (currentIns != null)
            {
                // get ins_courses for this instructor 
                var insCourses = itiContext.InsCourses.Where(c => c.InsId == id).ToList();
                // remove ins_courses for this instructor
                itiContext.InsCourses.RemoveRange(insCourses);               
                itiContext.Instructors.RemoveRange(currentIns);
                itiContext.SaveChanges();                
            }
            return RedirectToAction("index");

        }
    }
}
