using CourseAllocationCoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Subject> subjects = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var resultTask = client.GetAsync("subject");
                resultTask.Wait();
                var result = resultTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Subject>>();
                    readTask.Wait();
                    subjects = readTask.Result;
                }
                else
                {
                    subjects = Enumerable.Empty<Subject>();
                    ModelState.AddModelError(string.Empty, "server Error.Please contact admin");
                }
            }
            return View(subjects);
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/subject");
                    var post = client.PostAsJsonAsync<Subject>("subject", subject);
                    post.Wait();
                    var result = post.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return View();
        }

        public IActionResult Delete(string id)
        {
            ViewBag.Id = id;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var deleteTask = client.DeleteAsync("subject?SubjectId=" + id);

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.status = result.ReasonPhrase + " " + result.Content;
                }
            }
            return View();
        }

    }
}
