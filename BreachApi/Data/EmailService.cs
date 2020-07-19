using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BreachApi.Data.Interfaces;
using BreachApi.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace BreachApi.Data
{
    [ExcludeFromCodeCoverage]
    public class EmailService : IEmailService
    {
        public SqlConnection _conn { get; set; }
        public EmailService(IOptions<AppSettings> config)
        {
            _conn = new SqlConnection(config.Value.ConnectionString);
        }

        public async Task<BreachedEmailApiModel> Get(string email)
        {
            var sql = "select * from BreachedEmail WHERE Email = @email";
            return await _conn.QueryFirstOrDefaultAsync<BreachedEmailApiModel>(
                sql,
                new
                {
                    email
                });
        }

        public async Task<int> Insert(BreachedEmailApiModel model)
        {
            var sql = "INSERT INTO BreachedEmail (Email) values (@email);SELECT CAST(SCOPE_IDENTITY() as int);";
            return await _conn.QuerySingleOrDefaultAsync<int>(
                sql,
                model);
        }

        public async Task Delete(string email)
        {
            var sql = "DELETE FROM BreachedEmail WHERE Email = @email";
            await _conn.ExecuteAsync(
                sql,
                new
                {
                    email
                });
        }
    }
}
