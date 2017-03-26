using Dal.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Model
{
    public class LoginDbContext : IdentityDbContext<QuizUser>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options): base(options)
        {

        }
    }
}
