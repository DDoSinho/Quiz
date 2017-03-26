﻿using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dal
{
    [Table("Questions")]
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.GivedAnswers = new List<GivedAnswer>();
        }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public Nullable<int> ThemeId { get; set; }

        public virtual Theme  Theme { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<GivedAnswer> GivedAnswers { get; set; }
    }
}