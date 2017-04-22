using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using System.IO;
using Newtonsoft.Json;
using Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class QuizApiController : Controller
    {
        private readonly IQuizService _quizService;

        public QuizApiController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public IActionResult GetQuizs()
        {
            return Ok(_quizService.GetQuizs());
        }
    }
}
