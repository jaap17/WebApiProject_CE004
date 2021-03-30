using CourseAllocationCoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Faculty> faculties = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var resultTask = client.GetAsync("faculty");
                resultTask.Wait();
                var result = resultTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Faculty>>();
                    readTask.Wait();
                    faculties = readTask.Result;
                }
                else
                {
                    faculties = Enumerable.Empty<Faculty>();
                    ModelState.AddModelError(string.Empty, "server Error.Please contact admin");
                }
            }
            return View(faculties);
           
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Faculty faculty)
        {
            string name = faculty.FirstName;
            faculty.Password = name + "@2000";
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/faculty");
                    var post = client.PostAsJsonAsync<Faculty>("faculty", faculty);
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
            
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var deleteTask = client.DeleteAsync("faculty?username="+id);

                var result = deleteTask.Result;
                if(result.IsSuccessStatusCode)
                {       
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.status = result.ReasonPhrase+ " "+result.Content;
                }
            }
            return View("Delete");
        }
    }
}
