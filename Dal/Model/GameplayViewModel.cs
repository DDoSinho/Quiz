using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Model
{
    public class GameplayViewModel
    {
        public Question Question { get; set; }

        public string Ids { get; set; }

        public int SessionId { get; set; }

        public int QuestionId { get; set; }

        public int Points { get; set; }
    }
}
