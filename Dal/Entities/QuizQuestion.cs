using Dal;
using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Entities
{
    [Table("QuizQuestions")]
    public class QuizQuestion
    {
        public QuizQuestion()
        {
            
        }

        public int QuizQuestionId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual Question Question { get; set; }
    }
}
