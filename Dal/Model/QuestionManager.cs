using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dal.Entities;
using Dal.Model;
using Web.Entities;

namespace Dal
{
    public class QuestionManager
    {
        private QuizDbContext Context { get; }

        public QuestionManager(QuizDbContext context)
        {
            Context = context;
        }

        public void AddQuestion(Question question, Theme theme, Quiz quiz)
        {
            QuizQuestion quizquestion = new QuizQuestion();

            var QuizInDb = Context.Quizs
                           .Where(q => q.Name == quiz.Name)
                           .FirstOrDefault();

            if(QuizInDb == null)
            {
                Context.Add(quiz);
                quizquestion.Quiz = quiz;
            }
            else
            {
                quizquestion.Quiz = QuizInDb;
            }

            var ThemeInDb = Context.Themes
                               .Where(t => t.Name == theme.Name)
                               .FirstOrDefault();

            if(ThemeInDb == null)
            {
                Context.Add(theme);
                theme.Questions.Add(question);
            }
            else
            {
                ThemeInDb.Questions.Add(question);
            }

            quizquestion.Question = question;

            Context.Add(quizquestion);

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

        public void AddQuiz(Quiz quiz)
        {
            Context.Add(quiz);
            Context.SaveChanges();
        }

        public void AddQuizQuestion(QuizQuestion quizquestion)
        {
            Context.Add(quizquestion);
            Context.SaveChanges();
        }

        public List<Theme> GetThemes()
        {
            var query = from q in Context.Themes
                        select q;

            return query.ToList();
        }

        public List<Quiz> GetQuizs()
        {
            return Context.Quizs.Select(q => q).ToList();
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
            //TODO: calculate the number of good answers and the number of all answers
            //return 100-(good_ans/all_ans)*100

            return 1.0;
        }

    }
}