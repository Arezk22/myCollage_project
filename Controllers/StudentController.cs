using asp.netDay2.Models;
using asp.netDay2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.netDay2.ViewModel;
namespace asp.netDay2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ItiContext itiContext;
        public StudentController(ItiContext context)
        {
            itiContext = context;
        }
        public IActionResult Index()
        {
            ICollection<Student> students = itiContext.Students.Take(100).ToList();

            return View(students);
        }        
        public IActionResult Details(int id)
        {
            var student = itiContext.Students.Where(s => s.StId == id).Include(s => s.Dept).FirstOrDefault();
            var department = student.Dept;
            StudentVM studentVM = new StudentVM()
            {
                StudentName = student.StFname + " " + student.StLname,
                StudentDepartment = department.DeptName,
                birthYear = 2024 - student.StAge
            };
            itiContext.SaveChanges();           
            return View(studentVM);
        }

        public IActionResult Create()
        {
            // return the create view to enter data
            return View();
        }

        [HttpPost]
        public IActionResult Save(Student student)
        {
            if (ModelState.IsValid)
            {
                // create new student using context
                itiContext.Students.Add(student);
                itiContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            // 1- get student by id
            var student = itiContext.Students.Where(s => s.StId == id).FirstOrDefault();
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            var currentStudent = itiContext.Students.Where(s => s.StId == student.StId).FirstOrDefault();
            currentStudent.StFname = student.StFname;
            currentStudent.StLname = student.StLname;            
            itiContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var currentStudent = itiContext.Students.Where(s => s.StId == id).FirstOrDefault();
            if (currentStudent != null)
            {
                // get stud_courses for this student 
                var studentCourses = itiContext.StudCourses.Where(c => c.StId == id).ToList();

                // remove stud_courses for this student
                itiContext.StudCourses.RemoveRange(studentCourses);
                var students = itiContext.Students.Where(c => c.StSuper == id).ToList();
                foreach (var student in students)
                {
                    student.StSuper = null;
                    itiContext.SaveChanges();
                }
                // remove the student
                itiContext.Students.Remove(currentStudent);

                itiContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
         
    }
}
