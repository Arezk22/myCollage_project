using asp.netDay2.Models;
using asp.netDay2.ViewModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace asp.netDay2.Controllers
{
    public class TopicController : Controller
    {
        private readonly ItiContext itiContext;
        public TopicController(ItiContext context)
        {
            itiContext = context;
        }
        public IActionResult Index()
        {
            ICollection<Topic> Topics = itiContext.Topics.Take(100).ToList();
            return View(Topics);
        }
        public IActionResult Details(int id)
        {
            var topic = itiContext.Topics.Where(s => s.TopId == id).FirstOrDefault();

            TopicVM topicVM = new TopicVM()
            {
                TopicID = topic.TopId,
                TopicName = topic.TopName,
                courses = topic.Courses.ToString()
            };
            itiContext.SaveChanges();
            return View(topicVM);
        }
        public IActionResult Create()
        {
            // return the create view to enter data
            return View();
        }
        [HttpPost]
        public IActionResult Save(Topic top)
        {
            if (ModelState.IsValid)
            {

                // create new instructor using context
                itiContext.Topics.Add(top);
                itiContext.SaveChanges();
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var currentTopic = itiContext.Topics.Where(i => i.TopId == id).FirstOrDefault();
            return View(currentTopic);
        }

        [HttpPost]
        public IActionResult Edit(Topic topic)
        {
            var currentTopic = itiContext.Topics.Where(i => i.TopId == topic.TopId).FirstOrDefault();
            currentTopic.TopId = topic.TopId;
            currentTopic.TopName = topic.TopName;            
            itiContext.SaveChanges();
            return RedirectToAction("index");
        }
        //public ActionResult delete(int id)
        //{
        //    var currentTopic = itiContext.Topics.Where(i => i.TopId == id).FirstOrDefault();
        //    if (currentTopic != null)
        //    {
                
        //        var topCourseIds = itiContext.Courses.Where(c => c.TopId == id).ToList();                              
        //        var studentCrs = itiContext.StudCourses.Where(c => topCourseIds.Any(d => d.CrsId == c.CrsId)).ToList();

        //        //var topCourseIds = itiContext.Courses.Where(c => c.TopId == id).Select(c => c.CrsId).ToList();

        //        // Step 2: Retrieve the student courses where the CrsId is in the list of top course IDs
        //        //var studentCrs = itiContext.StudCourses.Where(c => topCourseIds.Contains(c.CrsId)).ToList();
        //        itiContext.StudCourses.RemoveRange(studentCrs);
        //        itiContext.Topics.RemoveRange(currentTopic);
        //        itiContext.SaveChanges();
        //    }
        //    return RedirectToAction("index");

        //}
    }
}
