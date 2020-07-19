using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreachApi.IntegrationTest
{
    public class BreachApiTestFactory<T> : WebApplicationFactory<Startup>
    {
    }
}
