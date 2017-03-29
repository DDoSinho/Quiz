using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class AddQuestionController : Controller
    {
        private QuestionManager _questionManager;

        public AddQuestionController(QuestionManager questionManager)
        {
            _questionManager = questionManager;
        }

        public IActionResult Index()
        {
            return View(_questionManager.GetThemes());
        }

        [HttpPost]
        public IActionResult SaveQuestion([FromForm] Question question, [FromForm] Theme theme)
        {
            if (question == null || theme == null)
            {
                BadRequest();
            }

            _questionManager.AddQuestion(question, theme);

            return View("Answers", question);
        }

        [HttpPost]
        public IActionResult SaveAnswers(List<Answer> answer)
        {
            if (answer == null)
            {
                BadRequest();
            }

            foreach (var item in answer)
            {
                if (item.Text != null)
                {
                    _questionManager.AddAnswer(item);
                }
            }

            return View("Save");
        }
    }
}
