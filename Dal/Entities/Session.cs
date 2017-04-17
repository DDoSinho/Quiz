using Dal.Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dal.Entities
{
    [Table("Sessions")]
    public class Session
    {
        public Session()
        {
            this.GivedAnswers = new List<GivedAnswer>();
        }

        public int SessionId { get; set; }

        public ICollection<GivedAnswer> GivedAnswers { get; set; }

        public virtual QuizUser QuizUser { get; set; }

        public virtual Quiz Quiz { get; set; }

        public int Point { get; set; }

    }
}
