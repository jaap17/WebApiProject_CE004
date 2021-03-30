using CourseAllocationCoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Controllers
{
    public class FacultyChoiceController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            
            IEnumerable<Subject> subjects = null;
            string type = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var resultTask = client.GetAsync("type");
                resultTask.Wait();
                var result = resultTask.Result;

                var readTask = result.Content.ReadAsAsync<string>();
                readTask.Wait();
                type = readTask.Result;
            }
            if(type.Equals("odd"))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/");
                    var resultTask = client.GetAsync("oddsubject");
                    resultTask.Wait();
                    var result = resultTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IEnumerable<Subject>>();
                        readTask.Wait();
                        subjects = readTask.Result;
                        ViewBag.subjects = subjects;
                    }
                    else
                    {
                        subjects = Enumerable.Empty<Subject>();
                        ModelState.AddModelError(string.Empty, "server Error.Please contact admin");
                    }
                }
                
            }
            else if (type.Equals("even"))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/");
                    var resultTask = client.GetAsync("evensubject");
                    resultTask.Wait();
                    var result = resultTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IEnumerable<Subject>>();
                        readTask.Wait();
                        subjects = readTask.Result;
                        ViewBag.subjects = subjects;
                    }
                    else
                    {
                        subjects = Enumerable.Empty<Subject>();
                        ModelState.AddModelError(string.Empty, "server Error.Please contact admin");
                    }
                }
            }
            
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(FacultyChoice facultyChoice)
        {
                if (ModelState.IsValid)
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44336/api/facultychoice");
                        var post = client.PostAsJsonAsync<FacultyChoice>("facultychoice", facultyChoice);
                        post.Wait();
                        var result = post.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return View("ShowChoice",facultyChoice);
                        }

                    }
                }
                return View();
        }
           
        

        public IActionResult Index()
        {
            IEnumerable<FacultyChoice> facultychoices = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var resultTask = client.GetAsync("facultychoice");
                resultTask.Wait();
                var result = resultTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<FacultyChoice>>();
                    readTask.Wait();
                    facultychoices = readTask.Result;
                }
                else
                {
                    facultychoices = Enumerable.Empty<FacultyChoice>();
                    ModelState.AddModelError(string.Empty, "server Error.Please contact admin");
                }
            }
            return View(facultychoices);
        }
    }
}
