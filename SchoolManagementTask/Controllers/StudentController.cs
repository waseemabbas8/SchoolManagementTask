using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

                string APIURL = "http://bulksms.com.pk/api/sms.php?username=923338311685&password=2915&sender=BrandName&mobile="+student.Mobile+"&message=Welcome to our website.";
                
                using (var APIClient = new HttpClient())
                {
                    Task<HttpResponseMessage> RM = APIClient.GetAsync(APIURL);
                    Task<string> FinalRespone = RM.Result.Content.ReadAsStringAsync();
                }
                


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

        public string GetStudentsNames()
        {
            string Result = "";

            var r = Request;

            IList<Student> All =  ORM.Student.ToList<Student>();
            Result += "<h1 class='alert alert-success'>Total Students: "+All.Count+"</h1>";

            foreach(Student S in All)
            {
                Result += "<a href='/Student/StudentDetail?Id="+S.Id+"'><p><span class='glyphicon glyphicon-user'></span> " + S.Name+"</p></a> <a href='/student/deletestudent1?id="+S.Id+"'>Delete</a>";
            }

            return Result;
        }

        public string ShowAd()
        {
            string Ad = "";
            Ad = "<img class='img img-responsive' src='http://lorempixel.com/400/400/sports/Theta-Solutions/'/>";
            return Ad;
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

        public IActionResult DeleteStudent1(Student student)
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