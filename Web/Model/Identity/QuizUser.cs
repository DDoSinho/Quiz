using Dal.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Model.Identity
{
    public class QuizUser : IdentityUser
    {
        public QuizUser()
        {
            this.Sessions = new List<Session>();
        }

        public ICollection<Session> Sessions { get; set; }
    }
}
