using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dal
{
    [Table("Answers")]
    public class Answer
    {
        public Answer()
        {
            this.GivedAnswers = new List<GivedAnswer>();
        }

        public int AnswerId { get; set; }

        public Nullable<int> QuestionId { get; set; }

        public string Text { get; set; }

        public bool IsGoodAnswer { get; set; }

        public virtual Question Question { get; set; }

        public ICollection<GivedAnswer> GivedAnswers { get; set; }
    }
}