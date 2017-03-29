using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities;

namespace Dal.Entities
{
    public class Quiz
    {
        public Quiz()
        {
            Sessions = new List<Session>();
            this.QuizQuestions = new List<QuizQuestion>();
        }

        public int QuizID { get; set; }

        public int Name { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
