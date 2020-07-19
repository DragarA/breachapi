using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreachApi.Models;

namespace BreachApi.Data.Interfaces
{
    public interface IEmailService
    {
        Task<BreachedEmailApiModel> Get(string email);
        Task<int> Insert(BreachedEmailApiModel email);
        Task Delete(string email);
    }
}
