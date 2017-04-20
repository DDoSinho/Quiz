using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dal;
using Dal.Model;
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
        private QuestionManager _questionManager;
        private UserManager<QuizUser> _userManager;

        public HomeController(QuestionManager questionManager, UserManager<QuizUser> userManager)
        {
            _questionManager = questionManager;
            _userManager = userManager;
        }

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

        public IActionResult Statistic()
        {

            return View(
                new StatisticViewModel()
                {
                    AllTopScore=_questionManager.GetAllTopScore(),
                    TopScore=_questionManager.GetTopScore(_userManager.GetUserId(User)),
                    HardestAndEasiestQuestion= _questionManager.GetHardestAndEasiestQuestion(),
                    MostPoplularQuiz=_questionManager.GetMostPopularQuizName()
                }
            );
        }
    }
}
