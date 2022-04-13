using StudentMVC.Models;
using StudentMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        private StudentServices _stuServices;
        // GET: Student
        public ActionResult List()
        {
            _stuServices = new StudentServices();

            var model = _stuServices.GetStudentList();

            return View(model);
        }

        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(StudentModel model)
        {
            _stuServices = new StudentServices();

            _stuServices.InsertStudent(model);

            return RedirectToAction("List");
        }

        public ActionResult EditStudent(int Student_ID = 0)
        {
            _stuServices = new StudentServices();

            var model = _stuServices.GetEditById(Student_ID);

            return View(model);
        }
        [HttpPost]
        public ActionResult EditStudent(StudentModel model)
        {
            _stuServices = new StudentServices();

            _stuServices.UpdateStudent(model);

            return RedirectToAction("List");
        }

        //[HttpPost]
        public ActionResult DeleteStudent(int Student_ID)
        {
            _stuServices = new StudentServices();

            _stuServices.DeleteStudent(Student_ID);

            return RedirectToAction("List");
        }

        private UserServices _userServices;

        // GET: Student

        public ActionResult AddUser(int Student_ID)
        {
            UserModel obj = new UserModel();

            obj.Student_ID = Student_ID;

            return View(obj);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel model)
        {
            _userServices = new UserServices();

            _userServices.CreateUser(model);

            return RedirectToAction("List");
        }

        public ActionResult EditUser(int Student_ID = 0)
        {
            _userServices = new UserServices();

            var model = _userServices.GetEditById(Student_ID);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserModel model)
        {
            _userServices = new UserServices();

            _userServices.UpdateUser(model);

            return RedirectToAction("List");
        }

        //[HttpPost]
        public ActionResult DeleteUser(int Student_ID = 0)
        {
            _userServices = new UserServices();

            _userServices.DeleteUser(Student_ID);

            return RedirectToAction("List");
        }
    }
}