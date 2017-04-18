using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dal;
using Dal.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Created()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}


