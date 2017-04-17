﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dal.Entities;
using Dal.Model;
using Web.Entities;
using Dal.Model.Identity;

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

            if (QuizInDb == null)
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

            if (ThemeInDb == null)
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
            return Context.Quizs.ToList();
        }

        public List<int> GetQuestionsIdByThemeName(string themeName)
        {
            var query = Context.Questions
                        .Where(q => q.Theme.Name == themeName)
                        .Select(q => q.QuestionId);

            return query.ToList();
        }

        public List<int> GetQuestionsIdByQuizId(string quizName)
        {
            var query = Context.QuizQuestions
                       .Where(q => q.Quiz.Name == quizName)
                       .Select(q => q.Question.QuestionId)
                       .ToList();
            return query;
        }

        public Question GetQuestionById(int id)
        {
            return Context.Questions
                   .Include(q => q.Answers)
                   .Where(q => q.QuestionId == id)
                   .SingleOrDefault();
        }

        public string GetThemeNameByQuestionId(int questionId)
        {
            return Context.Questions
                   .Where(q => q.QuestionId == questionId)
                   .Select(q => q.Theme.Name)
                   .Single();
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

        public int GetPoint(int questionId)
        {
            decimal countCorrect = 0;
            decimal countRecord = 0;

            var givedAnswers = Context.GivedAnswers
                               .Where(g => g.QuestionId == questionId)
                               .ToList();

            var sessionIds = givedAnswers.Select(g => g.SessionId).Distinct();

            foreach (var session in sessionIds)
            {
                List<GivedAnswer> givedAnswerList = givedAnswers.Where(g => g.SessionId == session).ToList();

                if (IsItGoodAnswers(givedAnswerList))
                {
                    countCorrect++;
                }

                countRecord++;
            }


            int point = 0;

            if (countRecord != 0) //0-val nem tudunk osztani
                point = (int)(100 - Math.Floor((countCorrect / countRecord) * 100));

            return point > 0 ? point : 1;
        }

        public void SetPhotoUrl(QuizUser user, string url)
        {
            user.PhotoUrl = url;
            Context.SaveChanges();
        }
    }
}