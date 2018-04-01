using System;
using Xunit;
using dataAPI.Controllers;
using System.Security.Claims;
using authAPI.Controllers;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace jwtsooTests
{
     public class DataTests
     {
          private static IConfiguration _configuration;
          public DataTests()
          {
               _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          }  //ctor

          [Fact]
          public void apiServesToValidToken()
          {
               //Arrange 1 of 3
               Claim[] claims = new[]
               {
                    new Claim(ClaimTypes.Name, "admin")
               };
               AuthController ac = new AuthController();
               string tokenstring = ac.GenerateJWT(claims, DateTime.Now);
               //Arrange 2 of 3
               DataController dc = new DataController();
               HttpControllerContext controllerContext = new HttpControllerContext();
               HttpRequestMessage request = new HttpRequestMessage();
               request.Headers.Add("Authorization", tokenstring);
               //controllerContext.Request = request;
               //dc.ControllerContext = controllerContext;
               //Arrange 3 of 3
               string expectedResponse = "This is your data";
               //Act
               var response = dc.GetData() as OkObjectResult;
               string content = response.Value as string;
               //Assert
               Assert.NotNull(response);
               Assert.Contains(expectedResponse, content);
          }

          [Fact (Skip = "Cannot test the Authorize attribute")]
          public void apiDoesntServeToInvalidToken()
          {
               //Arrange 1 of 2
               Claim[] claims = new[]
               {
                    new Claim(ClaimTypes.Name, "wrong")
               };
               AuthController ac = new AuthController();
               string tokenstring = ac.GenerateJWT(claims, DateTime.Now);
               //Arrange 2 of 2
               DataController dc = new DataController();
               HttpControllerContext controllerContext = new HttpControllerContext();
               HttpRequestMessage request = new HttpRequestMessage();
               request.Headers.Add("Authorization", tokenstring);
               //Act
               IActionResult response = dc.GetData();
               //Assert
               Assert.IsType<UnauthorizedResult>(response);
          }

          [Fact(Skip = "Cannot test the Authorize attribute")]
          public void apiDoesntServeToMissingToken()
          {
               DataController dc = new DataController();
               HttpControllerContext controllerContext = new HttpControllerContext();
               HttpRequestMessage request = new HttpRequestMessage();
               //Act
               IActionResult response = dc.GetData();
               //Assert
               Assert.IsType<UnauthorizedResult>(response);
          }

     }  //class
}  //namespace
