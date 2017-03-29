using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Model
{
    public class AddQuestionViewModel
    {
        public List<Theme> Themes { get; set; }

        public List<Quiz> Quizs { get; set; }
    }
}
