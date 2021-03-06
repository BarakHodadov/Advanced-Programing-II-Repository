﻿using ImageService;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            string path = HostingEnvironment.MapPath("~/App_Data/Details.txt");

            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                    students.Add(new Student(line));
            }
            catch (Exception)
            {

            }
            return students;
        }

        static List<Student> students = GetStudents();

        // GET: First/Details
        public ActionResult ImageWeb()
        {
            students = GetStudents();
            ImageWebModel model = new ImageWebModel(students);
            return View(model);
        }


        public ActionResult Config()
        {
            ConfigModel model = new ConfigModel();
            return View(model);
        }

        public ActionResult Logs()
        {
            LogsModel model = new LogsModel();
            return View(model);
        }


        public ActionResult Photos()
        {
            PhotosModel model = new PhotosModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult PhotoRemover(string path)
        {
            PhotoRemoverModel model = new PhotoRemoverModel(path);
            return View(model);
        }



        [HttpPost]
        public void RemoveHandlerHelper(string HandlerPath = "")
        {
            RemoveHandlerModel rhModel = new RemoveHandlerModel(HandlerPath);
            rhModel.removeHandler();
        }

        [HttpGet]
        public ActionResult RemoveHandler(string HandlerPath = "")
        {
            return View("RemoveHandler", new RemoveHandlerModel(HandlerPath)); 
        }

        [HttpGet]
        public ActionResult DeletePhoto(string absolute_path)
        {
            //delete image
            System.IO.File.Delete(absolute_path);

            //delete thumbnail
            string thumImagePath = absolute_path.Replace(@"\Images", @"\Images\Thumbnails");
            System.IO.File.Delete(thumImagePath);

            return View("Photos", new PhotosModel());
        }
        public string GetFullImagePath(string path)
        {
            return path.Replace(@"Thumbnails\\", "\\");
        }

        [HttpPost]
        public void DeleteHandler(string HandlerPath)
        {
            RemoveHandlerModel m = new RemoveHandlerModel(HandlerPath);
            m.removeHandler();
        }

        #region Dor's code
        static List<Employee> employees = new List<Employee>()
        {
          new Employee  { FirstName = "Moshe", LastName = "Aron", Email = "Stam@stam", Salary = 10000, Phone = "08-8888888" },
          new Employee  { FirstName = "Dor", LastName = "Nisim", Email = "Stam@stam", Salary = 2000, Phone = "08-8888888" },
          new Employee   { FirstName = "Mor", LastName = "Sinai", Email = "Stam@stam", Salary = 500, Phone = "08-8888888" },
          new Employee   { FirstName = "Dor", LastName = "Nisim", Email = "Stam@stam", Salary = 20, Phone = "08-8888888" },
          new Employee   { FirstName = "Dor", LastName = "Nisim", Email = "Stam@stam", Salary = 700, Phone = "08-8888888" }
        };
        // GET: First
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxView()
        {
            return View();
        }

        [HttpGet]
        public JObject GetEmployee()
        {
            JObject data = new JObject();
            data["FirstName"] = "Kuky";
            data["LastName"] = "Mopy";
            return data;
        }

        [HttpPost]
        public JObject GetEmployee(string name, int salary)
        {
            foreach (var empl in employees)
            {
                if (empl.Salary > salary || name.Equals(name))
                {
                    JObject data = new JObject();
                    data["FirstName"] = empl.FirstName;
                    data["LastName"] = empl.LastName;
                    data["Salary"] = empl.Salary;
                    return data;
                }
            }
            return null;
        }

        // GET: First/Details
        public ActionResult Details()
        {
            return View(employees);
        }

        // GET: First/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: First/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                employees.Add(emp);

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: First/Edit/5
        public ActionResult Edit(int id)
        {
            foreach (Employee emp in employees) {
                if (emp.ID.Equals(id)) { 
                    return View(emp);
                }
            }
            return View("Error");
        }

        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee empT)
        {
            try
            {
                foreach (Employee emp in employees)
                {
                    if (emp.ID.Equals(id))
                    {
                        emp.copy(empT);
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: First/Delete/5
        public ActionResult Delete(int id)
        {
            int i = 0;
            foreach (Employee emp in employees)
            {
                if (emp.ID.Equals(id))
                {
                    employees.RemoveAt(i);
                    return RedirectToAction("Details");
                }
                i++;
            }
            return RedirectToAction("Error");
        }

        #endregion
    }
}
