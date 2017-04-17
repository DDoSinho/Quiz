using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dal.Model;

namespace Web.Migrations
{
    [DbContext(typeof(QuizDbContext))]
    [Migration("20170417082358_answerdatetime2")]
    partial class answerdatetime2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("AnswerDate");

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

            modelBuilder.Entity("Dal.Entities.Quiz", b =>
                {
                    b.Property<int>("QuizID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("QuizID");

                    b.ToTable("Quizs");
                });

            modelBuilder.Entity("Dal.Entities.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("QuizID");

                    b.Property<string>("QuizUserId");

                    b.HasKey("SessionId");

                    b.HasIndex("QuizID");

                    b.HasIndex("QuizUserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Dal.Model.Identity.QuizUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Web.Entities.QuizQuestion", b =>
                {
                    b.Property<int>("QuizQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("QuestionId");

                    b.Property<int?>("QuizID");

                    b.HasKey("QuizQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuizID");

                    b.ToTable("QuizQuestions");
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

            modelBuilder.Entity("Dal.Entities.Session", b =>
                {
                    b.HasOne("Dal.Entities.Quiz", "Quiz")
                        .WithMany("Sessions")
                        .HasForeignKey("QuizID");

                    b.HasOne("Dal.Model.Identity.QuizUser", "QuizUser")
                        .WithMany("Sessions")
                        .HasForeignKey("QuizUserId");
                });

            modelBuilder.Entity("Dal.Question", b =>
                {
                    b.HasOne("Dal.Theme", "Theme")
                        .WithMany("Questions")
                        .HasForeignKey("ThemeId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Dal.Model.Identity.QuizUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Dal.Model.Identity.QuizUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dal.Model.Identity.QuizUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Web.Entities.QuizQuestion", b =>
                {
                    b.HasOne("Dal.Question", "Question")
                        .WithMany("QuizQuestions")
                        .HasForeignKey("QuestionId");

                    b.HasOne("Dal.Entities.Quiz", "Quiz")
                        .WithMany("QuizQuestions")
                        .HasForeignKey("QuizID");
                });
        }
    }
}
