using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dal;

namespace Dal.Migrations
{
    [DbContext(typeof(QuizDbContext))]
    partial class QuizDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dal.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsGoodAnswer");

                    b.Property<int?>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Dal.Entities.GivedAnswer", b =>
                {
                    b.Property<int>("GivedAnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnswerId");

                    b.Property<bool>("Correct");

                    b.Property<int?>("QuestionId");

                    b.Property<int?>("SessionId");

                    b.HasKey("GivedAnswerId");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SessionId");

                    b.ToTable("GivedAnswers");
                });

            modelBuilder.Entity("Dal.Entities.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("SessionId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Dal.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.Property<int?>("ThemeId");

                    b.HasKey("QuestionId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Dal.Theme", b =>
                {
                    b.Property<int>("ThemeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ThemeId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("Dal.Answer", b =>
                {
                    b.HasOne("Dal.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Dal.Entities.GivedAnswer", b =>
                {
                    b.HasOne("Dal.Answer", "Answer")
                        .WithMany("GivedAnswers")
                        .HasForeignKey("AnswerId");

                    b.HasOne("Dal.Question", "Question")
                        .WithMany("GivedAnswers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("Dal.Entities.Session", "Session")
                        .WithMany("GivedAnswers")
                        .HasForeignKey("SessionId");
                });

            modelBuilder.Entity("Dal.Question", b =>
                {
                    b.HasOne("Dal.Theme", "Theme")
                        .WithMany("Questions")
                        .HasForeignKey("ThemeId");
                });
        }
    }
}
