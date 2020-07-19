using BreachApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BreachApi.Data.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace WebApiTest
{
    class EmailServiceFake : IEmailService
    {
        public List<BreachedEmailApiModel> _list;
        public EmailServiceFake(IOptions<AppSettings> config)
        {
            _list = new List<BreachedEmailApiModel>()
            {
                new BreachedEmailApiModel
                {
                    Id = 1,
                    Email = "anze.dragar@rrc.si"
                },
                new BreachedEmailApiModel
                {
                    Id = 1,
                    Email = "anze.dragar@test.si"
                },
            };
        }

        public async Task<BreachedEmailApiModel> Get(string email)
        {
            return _list.FirstOrDefault(i => i.Email == email);
        }

        public async Task<int> Insert(BreachedEmailApiModel model)
        {
            var id = _list.Min(i => i.Id);
            model.Id = id + 1;
            _list.Add(model);
            return id;
        }

        public Task Delete(string email)
        {
            var item = _list.FirstOrDefault(i => i.Email == email);
            _list.Remove(item);
            return Task.CompletedTask;
        }
    }
}
