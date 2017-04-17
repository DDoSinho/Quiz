using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dal.Entities
{
    [Table("GivedAnswers")]
    public class GivedAnswer
    {
        public int GivedAnswerId { get; set; }

        public Nullable<int> QuestionId { get; set; }

        public Nullable<int> AnswerId { get; set; }

        public Nullable<int> SessionId { get; set; }

        public bool Correct { get; set; }

        public virtual Question Question { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Session Session { get; set; }

        public DateTime AnswerDate { get; set; } = DateTime.Now;
    }
}
