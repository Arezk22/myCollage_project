using asp.netDay2.Models;
using asp.netDay2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.netDay2.Controllers
{    
    public class DepartmentController : Controller
    {

        private readonly ItiContext itiContext;
        public DepartmentController(ItiContext Context)
        {
            itiContext = Context;
        }
        public IActionResult Index()
        {
            ICollection<Department> departments = itiContext.Departments.Take(100).ToList();
            return View(departments);
        }
        public IActionResult Details(int id)
        {
            var department = itiContext.Departments.Where(d => d.DeptId == id).FirstOrDefault();
            //var Top = course.Top;
            DepartmentVM DptVM = new DepartmentVM()
            {
                DeptName = department.DeptName,
                DeptLocation = department.DeptLocation,                
            };
            itiContext.SaveChanges();
            return View(DptVM);
        }

        public IActionResult Create()
        {
            // return the create view to enter data
            return View();
        }
        [HttpPost]
        public IActionResult Save(Department Dept)
        {
            if (ModelState.IsValid)
            {

                // create new instructor using context
                itiContext.Departments.Add(Dept);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var currentDept = itiContext.Departments.Where(d => d.DeptId == id).FirstOrDefault();
            return View(currentDept);
        }

        [HttpPost]
        public IActionResult Edit(Department Department)
        {
            var currentDept = itiContext.Departments.Where(d => d.DeptId == Department.DeptId).FirstOrDefault();
            currentDept.DeptName = Department.DeptName;
            currentDept.DeptDesc = Department.DeptDesc;
            currentDept.DeptManager = Department.DeptManager;
            currentDept.DeptLocation = Department.DeptLocation;
            itiContext.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult delete(int id)
        {
            var currentDept = itiContext.Departments.Where(d => d.DeptId == id).FirstOrDefault();
            
            if (currentDept != null)
            {
                
                var insDept = itiContext.Instructors.Where(c => c.DeptId == id);
                var inscrs = itiContext.InsCourses.Where(c => insDept.Any(d => d.InsId == c.InsId));                
                
                // remove ins_courses for this department
                itiContext.Instructors.RemoveRange(insDept);
                //itiContext.Students.RemoveRange(studentDept);
                itiContext.InsCourses.RemoveRange(inscrs);
                itiContext.Departments.RemoveRange(currentDept);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");

        }

    }
}
