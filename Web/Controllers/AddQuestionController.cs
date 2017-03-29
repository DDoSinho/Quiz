using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using Microsoft.AspNetCore.Authorization;
using Dal.Model;
using Dal.Entities;

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
            AddQuestionViewModel model = new AddQuestionViewModel()
            {
                Themes = _questionManager.GetThemes(),
                Quizs = _questionManager.GetQuizs()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveQuestion([FromForm] Question question, [FromForm] Theme theme, [FromForm] Quiz quiz)
        {
            if (quiz == null || question == null || theme == null)
            {
                BadRequest();
            }

            _questionManager.AddQuestion(question, theme, quiz);

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
