using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dal.Entities;

namespace Dal
{
    public class QuestionManager
    {
        private QuizDbContext Context { get; }

        public QuestionManager(QuizDbContext context)
        {
            Context = context;
        }

        public void AddQuestion(Question question, Theme theme)
        {
            var ThemeInDb = Context.Themes
                               .Where(t => t.Name == theme.Name)
                               .FirstOrDefault();

            if (ThemeInDb == null)
            {
                Context.Add(theme);
                theme.Questions.Add(question);
                Context.SaveChanges();
                return;
            }

            ThemeInDb.Questions.Add(question);
            Context.SaveChanges();
        }

        public void AddAnswer(Answer answer)
        {
            Context.Add(answer);
            Context.SaveChanges();
        }

        public void AddGivedAnswer(GivedAnswer givedanswer)
        {
            Context.Add(givedanswer);
            Context.SaveChanges();
        }

        public void AddSession(Session session)
        {
            Context.Add(session);
            Context.SaveChanges();
        }

        public List<Theme> GetThemes()
        {
            var query = from q in Context.Themes
                        select q;

            return query.ToList();
        }

        public List<int> GetQuestionsIdByThemeName(string themeName)
        {
            var query = Context.Questions
                        .Where(q => q.Theme.Name == themeName)
                        .Select(q => q.QuestionId);

            return query.ToList();
        }

        public Question GetQuestionById(int id)
        {
            return Context.Questions
                   .Include(q => q.Answers)
                   .Where(q => q.QuestionId == id)
                   .FirstOrDefault();
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            var query = from q in Context.Answers
                        where q.Question.QuestionId == questionId
                        select q;

            return query.ToList();
        }

        public bool IsItGoodAnswers(List<GivedAnswer> givedAnswerList)
        {
            foreach (var givedans in givedAnswerList)
            {
                var query = Context.Answers
                            .Where(a => a.AnswerId == givedans.AnswerId)
                            .Where(a => a.IsGoodAnswer == givedans.Correct)
                            .FirstOrDefault();

                if (query == null)
                {
                    return false;
                }
            }

            return true;
        }

        public double GetPoint(int questionId)
        {
            var answers = Context.GivedAnswers
                .Where(g => g.QuestionId == questionId)
                .Select(a => a.Answer.IsGoodAnswer).ToList();

            var correct = answers.Count(a => a);
            var all = answers.Count;

            List<int> sessionIds = Context.Sessions
                                   .Select(s => s.SessionId)
                                   .ToList();

            double CountCorrect = 0;
            double CountRecords = 0;

            foreach (var sessionid in sessionIds)
            {
                List<GivedAnswer> givedAnswerList = Context.GivedAnswers
                                                    .Where(a => a.QuestionId == questionId)
                                                    .Select(a => a).ToList();

                if (IsItGoodAnswers(givedAnswerList))
                {
                    CountCorrect++;
                }

                CountRecords++;
            }

            return 100 - Math.Floor((CountCorrect / CountRecords) * 100);
        }

    }
}