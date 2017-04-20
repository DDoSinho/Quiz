using Dal;
using Dal.Entities;
using Dal.Model;
using Dal.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private QuestionManager _questionManager;
        private UserManager<QuizUser> _userManager;
        private const string SessionKeyPoints = "_Points";
        private const string SessionKeySession = "_Session";
        private const string SessionKeyIds = "_Ids";
        private const string SessionKeyQuestionId = "_QuestionId";

        public GameController(QuestionManager questionManager, UserManager<QuizUser> userManager)
        {
            _questionManager = questionManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ChooseQuiz()
        {
            return View(_questionManager.GetQuizs());
        }

        [HttpPost]
        public async Task<IActionResult> InitGame([FromForm] Quiz quiz)
        {
            Session session = new Session()
            {
                QuizUser = await _userManager.GetUserAsync(User),
                Quiz=_questionManager.GetQuizByName(quiz.Name)
            };
            _questionManager.AddSession(session);

            int iiiiid = session.SessionId;
            HttpContext.Session.SetInt32(SessionKeySession, session.SessionId);

            List<int> questionIds = _questionManager.GetQuestionsIdByQuizId(quiz.Name);

            int questionId = questionIds[0];
            questionIds.RemoveAt(0);

            HttpContext.Session.SetInt32(SessionKeyPoints, 0);

            string ids = AppendIds(questionIds);
            HttpContext.Session.SetString(SessionKeyIds, ids);

            ViewData["quiz"] = quiz.Name;
            ViewData["theme"] = _questionManager.GetThemeNameByQuestionId(questionId);

            return View("Gameplay", new GameplayViewModel()
            {
                Question = _questionManager.GetQuestionById(questionId),
                Ids = ids,
                SessionId = session.SessionId,
                Points = 0
            });
        }

        private string AppendIds(List<int> questionIds)
        {
            StringBuilder idsConcatenat = new StringBuilder();

            for (int i = 0; i < questionIds.Count; i++)
            {
                if (i != questionIds.Count - 1)
                    idsConcatenat.Append(questionIds[i].ToString() + " ");
                else
                    idsConcatenat.Append(questionIds[i].ToString());
            }

            return idsConcatenat.ToString();
        }

        [HttpPost]
        public IActionResult Gameplay([FromForm]  int questionId, [FromForm] List<GivedAnswer> givedAnswerList)
        {
            GameplayViewModel vmodel = new GameplayViewModel();

            if (givedAnswerList != null)
            {
                if (_questionManager.IsItGoodAnswers(givedAnswerList))
                {
                    int? point;
                    point = HttpContext.Session.GetInt32(SessionKeyPoints);
                    point += _questionManager.GetPoint(questionId);
                    HttpContext.Session.SetInt32(SessionKeyPoints, point ?? 0);
                    vmodel.Points = point ?? 0;
                }
                else
                {
                    vmodel.Points = HttpContext.Session.GetInt32(SessionKeyPoints) ?? 0;
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

            string sessissonKeyIds = HttpContext.Session.GetString(SessionKeyIds);

            if (sessissonKeyIds == null || sessissonKeyIds.Equals(""))
            {
                ViewData["point"] = vmodel.Points;

                int? sessionsss = HttpContext.Session.GetInt32(SessionKeySession);

                _questionManager.AddPoint(
                    _questionManager.GetSessionById(HttpContext.Session.GetInt32(SessionKeySession) ?? 0),
                    vmodel.Points
                );

                return View("EndQuiz");
            }

            string[] splitedIds = sessissonKeyIds.Split(' ');
            int queryId = Int32.Parse(splitedIds[0]);

            string ids = ConcatenateIds(splitedIds);
            HttpContext.Session.SetString(SessionKeyIds, ids);

            ViewData["theme"] = _questionManager.GetThemeNameByQuestionId(queryId);

            return View("Gameplay", new GameplayViewModel()
            {
                Question = _questionManager.GetQuestionById(queryId),
                Ids = ids,
                SessionId = HttpContext.Session.GetInt32(SessionKeySession) ?? 0,
                Points = vmodel.Points
            });
        }

        private string ConcatenateIds(string[] splitedIds)
        {
            StringBuilder idsConcatenat = new StringBuilder();

            for (int i = 1; i < splitedIds.Length; i++)
            {
                if (i != splitedIds.Length - 1)
                    idsConcatenat.Append(splitedIds[i] + " ");
                else
                    idsConcatenat.Append(splitedIds[i]);
            }

            return idsConcatenat.ToString();
        }

    }
}
