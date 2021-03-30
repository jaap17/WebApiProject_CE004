using CourseAllocationCoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseAllocationCoreClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
           
            if (ModelState.IsValid)
            {
                if(login.Username.Equals("pankajjalote@gmail.com") && login.Password.Equals("admin123"))
                {
                    return View("AdminHomepage");
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44336/api/login");
                    var post = client.PostAsJsonAsync<Login>("login", login);
                    post.Wait();
                    var result = post.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return View("Homepage",login);
                    }

                }
            }
            return View();
        }

        public IActionResult ViewAllocation(string username)
        {
            ViewBag.Name = username;
            IEnumerable<Allocation> allocations1 = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44336/api/");
                var resultTask = client.GetAsync("allocation");
                resultTask.Wait();
                var result = resultTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Allocation>>();
                    readTask.Wait();
                    allocations1 = readTask.Result;
                }
            }
            return View("ShowAllocation",allocations1);
        }

        public IActionResult AdminHome()
        {
            return View("AdminHomepage");
        }
    }

  

}
