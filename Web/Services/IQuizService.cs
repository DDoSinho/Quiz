using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IQuizService
    {
        IEnumerable<Quiz> GetQuizs();
    }
}
