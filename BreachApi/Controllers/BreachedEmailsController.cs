using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using BreachApi.Data;
using BreachApi.Data.Interfaces;
using BreachApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BreachApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreachedEmailsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IEmailService _data;
        private readonly IMemoryCache _cache;
        public BreachedEmailsController(IEmailService data, IMemoryCache cache)
        {
            _data = data;
            _cache = cache;
        }

        [HttpGet("~/BreachedEmails/{email}")]
        public async Task<IActionResult> Get(string email)
        {

            BreachedEmailApiModel result;
            if (!_cache.TryGetValue<BreachedEmailApiModel>(email, out result))
            {
                result = await _data.Get(email);
                if (result == null)
                    return NotFound();

                _cache.Set<BreachedEmailApiModel>(email, result);
            }
            
            return Ok();
        }

        [HttpPost("~/BreachedEmails")]
        public async Task<IActionResult> Post(BreachedEmailApiModel model)
        {
            if (ModelState.IsValid)
            {
                var existing = await _data.Get(model.Email);

                if (existing != null)
                    return Conflict();

                model.Id = await _data.Insert(model);
                _cache.Set<BreachedEmailApiModel>(model.Email, model);
                return Created($"{nameof(Post)}/{model.Email}", model);
            }

            return BadRequest(ModelState);

        }

        [HttpDelete("~/BreachedEmails/{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            await _data.Delete(email);
            _cache.Remove(email);
            return Ok();
        }
    }
}
