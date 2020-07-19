using BreachApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BreachApi.IntegrationTest
{
    public class ControllerIntegrationTest : IClassFixture<BreachApiTestFactory<Startup>>
    {
        HttpClient _client;
        public ControllerIntegrationTest(BreachApiTestFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void Delete_Email_Test()
        {
            var response = await _client.DeleteAsync("/BreachedEmails/anze.dragar@rrc.si");   
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Post_Email_Test()
        {

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/BreachedEmails");


            var model = new BreachedEmailApiModel
            {
                Email = "anze.dragar@rrc.si"
            };
            postRequest.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("email", responseString);
            Assert.Contains("anze.dragar@rrc.si", responseString);
        }

        [Fact]
        public async void Get_Email_Test()
        {

            var getRequest = new HttpRequestMessage(HttpMethod.Get, "/BreachedEmails/anze.dragar@rrc.si");

            var response = await _client.SendAsync(getRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}
