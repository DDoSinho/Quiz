using Dal.Entities;
using Dal.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities;

namespace Dal.Model
{
    public class QuizDbContext : IdentityDbContext<QuizUser>
    {
        public QuizDbContext(){}

        public QuizDbContext(DbContextOptions<QuizDbContext> options): base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<GivedAnswer> GivedAnswers { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Quiz> Quizs { get; set; }

        public DbSet<QuizQuestion> QuizQuestions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuizDataBase;Trusted_Connection=True;");
        }
    }
}
