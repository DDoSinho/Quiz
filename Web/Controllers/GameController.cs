using Dal;
using Dal.Entities;
using Dal.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Controllers
{
    public class GameController : Controller
    {
        private QuestionManager _questionManager;

        public GameController(QuestionManager questionManager)
        {
            _questionManager = questionManager;

        }

        [HttpGet]
        public IActionResult ChooseTheme()
        {
            return View(_questionManager.GetThemes());
        }

        [HttpPost]
        public IActionResult InitGame([FromForm] Theme theme)
        {
            Session session = new Session();
            _questionManager.AddSession(session);

            List<int> questionIds = _questionManager.GetQuestionsIdByThemeName(theme.Name);

            int queryId = questionIds[0];
            questionIds.RemoveAt(0);

            StringBuilder idsConcatenat = new StringBuilder();

            foreach (var item in questionIds)
            {
                idsConcatenat.Append(item.ToString() + " ");
            }

            return View("Gameplay", new GameplayViewModel()
            {
                Question = _questionManager.GetQuestionById(queryId),
                Ids = idsConcatenat.ToString(),
                SessionId = session.SessionId,
                Points = 0
            });
        }

        [HttpPost]
        public IActionResult Gameplay([FromForm]  GameplayViewModel vmodel, [FromForm] List<GivedAnswer> givedAnswerList)
        {
           

            if (givedAnswerList != null)
            {
                if (_questionManager.IsItGoodAnswers(givedAnswerList))
                {
                    vmodel.Points += _questionManager.GetPoint(vmodel.QuestionId);
                }

                foreach (var item in givedAnswerList)
                {
                    _questionManager.AddGivedAnswer(item);
                }
            }
            else
            {
                BadRequest();
            }

            if (vmodel.Ids == null)
            {
                ViewData["point"] = vmodel.Points;
                return View("EndQuiz");
            }

            string[] splitedIds = vmodel.Ids.Split(' ');
            int queryId = Int32.Parse(splitedIds[0]);

            StringBuilder idsConcatenat = new StringBuilder();

            for (int i = 1; i < splitedIds.Length; i++)
            {
                idsConcatenat.Append(splitedIds[i] + " ");
            }

            return View("Gameplay", new GameplayViewModel()
            {
                Question = _questionManager.GetQuestionById(queryId),
                Ids = idsConcatenat.ToString(),
                SessionId = vmodel.SessionId,
                Points = vmodel.Points
            });
        }

    }
}
