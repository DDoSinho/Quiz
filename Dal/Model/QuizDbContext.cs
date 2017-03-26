using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class QuizDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<GivedAnswer> GivedAnswers { get; set; }

        public DbSet<Session> Sessions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuizDataBase;Trusted_Connection=True;");
        }
    }
}