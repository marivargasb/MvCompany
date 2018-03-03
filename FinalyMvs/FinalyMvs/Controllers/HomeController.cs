using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalyMvs.Models;

namespace FinalyMvs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (Login.isActiv == true)
            {

                if (Login.isAdmin)
                {

                    return View("Views/Home/Contact.cshtml");
                }
                else
                {
                    return View("Views/Home/About.cshtml");
                }
            }
            return View();
        }

        public IActionResult About()
        {
            if (Login.isActiv == true)
            {

                if (Login.isAdmin)
                {

                    return View("Views/Home/Contact.cshtml");
                }
                else
                {
                    return View("Views/Home/About.cshtml");
                }
            }
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            if (Login.isActiv == true)
            {

                if (Login.isAdmin)
                {

                    return View("Views/Home/Contact.cshtml");
                }
                else
                {
                    return View("Views/Home/About.cshtml");
                }
            }
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
