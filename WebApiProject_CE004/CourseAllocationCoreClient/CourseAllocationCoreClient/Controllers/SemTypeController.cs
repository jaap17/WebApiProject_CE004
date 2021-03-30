using CourseAllocationCoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Controllers
{
    public class SemTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SemType semType)
        {
            if(semType.SemesterType.Equals("odd"))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/type");
                    var post = client.PostAsJsonAsync<SemType>("type", semType);
                    post.Wait();
                    var result = post.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Create", "FacultyChoice");
                    }

                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/type");
                    var post = client.PostAsJsonAsync<SemType>("type", semType);
                    post.Wait();
                    var result = post.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Create", "FacultyChoice");
                    }

                }
            }
            return View();
        }


       /* public IActionResult Odd()
        {
            
            return View("Error");
        }

        public IActionResult Even()
        {
            using (var client = new HttpClient())
            {
                string type = "even";
                client.BaseAddress = new Uri("https://localhost:44336/api/type");
                var post = client.PostAsJsonAsync<string>("type",type);
                post.Wait();
                var result = post.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create", "FacultyChoice");
                }
               
            }
            return View("Error");
        }*/
    }
}
