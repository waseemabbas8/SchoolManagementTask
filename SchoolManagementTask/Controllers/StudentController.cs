using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementTask.Models;

namespace SchoolManagementTask.Controllers
{
    public class StudentController : Controller
    {
        private dbLectureORM19AugContext ORM;
        public StudentController(dbLectureORM19AugContext ORM)
        {
            this.ORM = ORM;
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                ORM.Add(student);
                ORM.SaveChanges();
                ViewBag.Message = "Registration Done Succefully!";
                ModelState.Clear();
            }
            catch
            {
                ViewBag.Message = "Error! Could not Save";
            }
            return View();
        }

        public IActionResult AllStudents()
        {
            IList<Student> students = ORM.Student.ToList<Student>();
            return View(students);
        }

        public IActionResult StudentDetail(int Id)
        {
            return View(ORM.Student.Where(m=>m.Id.Equals(Id)).FirstOrDefault());
        }

        public IActionResult EditStudent(int id)
        {
            Student student = ORM.Student.Where(m => m.Id.Equals(id)).FirstOrDefault();
            return View(student);
        }
    }
}