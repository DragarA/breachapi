using System;
using BreachApi.Controllers;
using BreachApi.Data.Interfaces;
using BreachApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace WebApiTest
{
    public class ControllerTest
    {
        private IEmailService _service;
        private BreachedEmailsController _controller;

        public ControllerTest()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());

            _service = new EmailServiceFake(null);
            _controller = new BreachedEmailsController(_service, cache);
        }


        #region GET
        [Fact]
        public async void GetByEmail_Ok()
        {
            var response = await _controller.Get("anze.dragar@rrc.si");
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async void GetByEmail_NotFound()
        {
            var response = await _controller.Get("anze@rrc.si");
            Assert.IsType<NotFoundResult>(response);
        }
        #endregion GET

        #region POST
        [Fact]
        public async void Post_Ok()
        {
            var model = new BreachedEmailApiModel
            {
                Email = "anze.dragar@gmail.com"
            };
            var response = await _controller.Post(model);
            Assert.IsType<CreatedResult>(response);
        }

        [Fact]
        public async void Post_Duplicate_Conflict()
        {
            var model = new BreachedEmailApiModel
            {
                Email = "anze.dragar@gmail.com"
            };
            var response = await _controller.Post(model);
            Assert.IsType<CreatedResult>(response);


            var responseDuplicated =  await _controller.Post(model);
            Assert.IsType<ConflictResult>(responseDuplicated);
        }

        [Fact]
        public async void Post_EmailNull_BadRequest()
        {
            _controller.ModelState.AddModelError("Email", "Field is required");
            var model = new BreachedEmailApiModel
            {
                Email = null
            };
            var response = await _controller.Post(model);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        #endregion POST

        #region DELETE
        [Fact]
        public async void Delete_Ok()
        {
            var response = await _controller.Delete("anze.dragar@rrc.si");
            Assert.IsType<OkResult>(response);
            Assert.IsType<NotFoundResult>(await _controller.Get("anze.dragar@rrc.si"));
        }
        #endregion DELETE

    }
}
