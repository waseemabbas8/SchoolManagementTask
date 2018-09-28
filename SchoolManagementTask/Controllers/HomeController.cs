using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementTask.Models;

namespace SchoolManagementTask.Controllers
{
    public class HomeController : Controller
    {
        dbLectureORM19AugContext ORM;
        public HomeController(dbLectureORM19AugContext ORM)
        {
            this.ORM = ORM;
        }
        public IActionResult Index()
        {
            ViewBag.StudentsCount = ORM.Student.ToList<Student>().Count();
            //ViewBag.TeachersCount = ORM.Teacher.ToList<Student>().Count();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
