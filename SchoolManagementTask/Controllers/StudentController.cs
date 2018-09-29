﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementTask.Helpers;
using SchoolManagementTask.Models;
using SchoolManagementTask.Resources;

namespace SchoolManagementTask.Controllers
{
    public class StudentController : Controller
    {
        private dbLectureORM19AugContext ORM;
        private IHostingEnvironment ENV;
        public StudentController(dbLectureORM19AugContext ORM, IHostingEnvironment ENV)
        {
            this.ORM = ORM;
            this.ENV = ENV;
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student, IFormFile Picture)
        {
            //Profile Picture upload flow
            string PicturePath = "/WebData/Images/ProfilePictures/" + Guid.NewGuid().ToString() + Path.GetExtension(Picture.FileName);
            FileStream FS = new FileStream(ENV.WebRootPath + PicturePath, FileMode.Create);
            Picture.CopyTo(FS);
            FS.Close();
            student.Picture = PicturePath;
            try
            {
                ORM.Add(student);
                ORM.SaveChanges();
                ViewBag.Message = "Registration Done Succefully!";
                ModelState.Clear();

                //Email object
                MailMessage oEmail = new MailMessage();
                oEmail.From = new MailAddress(EmailIfo.SMTP_USER_NAME);
                oEmail.To.Add(new MailAddress(student.Email));
                oEmail.CC.Add(new MailAddress("username@example.com"));
                oEmail.Subject = "Welcome to ABC";
                oEmail.Body = "Dear " + student.Name + ",<br><br>" +
                    "Thanks for registering with ABC, We are glad to have you in our system." +
                    "<br><br>" +
                    "<b>Regards</b>,<br>ABC Team";
                ViewBag.EmailResponse = NotificationHandler.SendEmail(oEmail);
            }
            catch(Exception ex)
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

        [HttpPost]
        public IActionResult AllStudents(int RollNo, string Name, string Department)
        {
            IList<Student> students = ORM.Student.Where(m=>m.RollNo.Equals(RollNo) || m.Name.Contains(Name) || m.Class.Contains(Department)).ToList<Student>();
            return View(students);
        }

        public IActionResult StudentDetail(int Id)
        {
            return View(ORM.Student.Where(m=>m.Id.Equals(Id)).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            Student student = ORM.Student.Where(m => m.Id.Equals(id)).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            try
            {
                ORM.Student.Update(student);
                ORM.SaveChanges();
                ViewBag.MessageSucess = "Record Updated Succefully";
            }
            catch
            {
                ViewBag.MessageFailure = "Error! Could not Update Record";
            }
            return View(student);
        }

        public IActionResult DeleteStudent(Student student)
        {
            ORM.Student.Remove(student);
            ORM.SaveChanges();
            return RedirectToAction(nameof(StudentController.AllStudents));
        }

        public string DeleteStudentByAjax(Student s)
        {
            string result;
            try
            {
                ORM.Student.Remove(s);
                ORM.SaveChanges();
                result = "Yes";
            }
            catch
            {
                result = "No";
            }
            return result;
        }
    }
}