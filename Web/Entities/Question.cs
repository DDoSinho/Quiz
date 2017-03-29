using Dal.Entities;
using Dal.Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities;

namespace Dal
{
    [Table("Questions")]
    public class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
            this.GivedAnswers = new List<GivedAnswer>();
            this.QuizQuestions = new List<QuizQuestion>();
        }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public Nullable<int> ThemeId { get; set; }

        public virtual Theme  Theme { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<GivedAnswer> GivedAnswers { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}