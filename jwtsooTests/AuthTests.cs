using System;
using Xunit;
using authAPI.Controllers;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace jwtsooTests
{
     public class AuthTests
     {
          private static IConfiguration _configuration;
          public AuthTests()
          {
               _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          }  //ctor

          [Fact]
          public void GenerateJWTGeneratesToken()
          {
               Claim[] claims = new[]
               {
                    new Claim(ClaimTypes.Name, "admin")
               };
               AuthController ac = new AuthController();
               //
               string tokenstring = ac.GenerateJWT(claims, DateTime.Now);
               //
               Assert.NotNull(tokenstring);
               Assert.IsType<string>(tokenstring);
          }

          [Fact]
          public void apiServesTokenToAuthorized()
          {
               TokenRequest request = new TokenRequest { Username = "admin", Password = "secret" };
               AuthController ac = new AuthController();
               //
               var response = ac.RequestToken(request);
               //
               Assert.IsType<OkObjectResult>(response);
          }

          [Fact]
          public void apiDoesntServeTokenToWrongUser()
          {
               TokenRequest request = new TokenRequest { Username = "wrong", Password = "secret" };
               AuthController ac = new AuthController();
               //
               var response = ac.RequestToken(request);
               //
               Assert.IsType<UnauthorizedResult>(response);
          }
          [Fact]
          public void apiDoesntServeTokenToWrongPassword()
          {
               TokenRequest request = new TokenRequest { Username = "admin", Password = "wrong" };
               AuthController ac = new AuthController();
               //
               var response = ac.RequestToken(request);
               //
               Assert.IsType<UnauthorizedResult>(response);
          }
     }  //class
}  //namespace
