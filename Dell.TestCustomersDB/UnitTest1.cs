using Dell.Data;
using Dell.Data.Entities;
using Dell.Data.EntityDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;

namespace Dell.TestCustomersDB
{
    public class CustDBTest
    {
        public IConfiguration Configuration { get; set; }

        string connStr;

        private readonly ITestOutputHelper output;

        public CustDBTest(ITestOutputHelper output)
        {
            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var config = Configuration.GetSection("Config").Get<Config>();
            connStr = config.Connections[config.Connections.Default].ConnectionString;
            output.WriteLine("Connection strins is: {0}", connStr);
        }

        [Fact]
        public void TestAddNew()
        {
            Customer entity = new Customer()
            {
                Age = 24,
                EmailAddress= "xxx",
                Name="Vlad"
            };

            Assert.Equal(1, new CustomersDB(connStr).Insert(entity));

        }

        [Fact]
        public void TestUpdate()
        {
            Customer entity2 = new Customer()
            {
                ID = 1,
                Age = 24,
                EmailAddress = $"{new Random().Next(2, 99999)}.vlad@gmail.ro",
                Name = "Vlad"
            };

            Assert.Equal(1, new CustomersDB(connStr).Update(entity2));
        }

        

         

    }
}
