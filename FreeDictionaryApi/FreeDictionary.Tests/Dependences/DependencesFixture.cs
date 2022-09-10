using FreeDictionary.Application.Model;
using FreeDictionary.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Tests.Dependences
{
    public class DependencesFixture
    {
        public HttpClient Client;

        public DependencesFixture()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            Client = webAppFactory.CreateDefaultClient();
        }
    }
}
