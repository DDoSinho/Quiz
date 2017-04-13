using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using System.IO;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class ApiController : Controller
    {
        private QuestionManager _questionManager;

        public ApiController(QuestionManager questionManager)
        {
            _questionManager = questionManager;
        }

        public string Index()
        {
            List<Theme> themes = _questionManager.GetThemes();

            string json = JsonConvert.SerializeObject(themes);

            return json;
        }
    }
}
