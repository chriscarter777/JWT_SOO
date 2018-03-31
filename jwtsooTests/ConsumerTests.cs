using System;
using Xunit;
using jwtsoo.Pages;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace jwtsooTests
{
     public class ConsumerTests
     {
          private static IConfiguration _configuration;
          public ConsumerTests()
          {
               _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          }  //ctor

          [Fact]
          public void ConsumerMakesAuthRequest()
          {
               IndexModel im = new IndexModel();
               //
               var result = im.OnPostGetTokenAsync();
               //
               Assert.NotNull(result);
          }

          [Fact]
          public void ConsumerMakesDataRequest()
          {
               IndexModel im = new IndexModel();
               //
               var result = im.OnPostGetDataAsync();
               //
               Assert.NotNull(result);
          }
     }  //class
}  //namspace