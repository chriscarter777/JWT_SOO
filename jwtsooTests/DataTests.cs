using System;
using Xunit;
using dataAPI;

namespace JWTauthTests
{
     public class DataTests
     {
          [Fact]
          public void apiServesToValidToken()
          {
               string token = "";
               int expectedCode = 200;
               string expectedData = "This is your data.";
               Assert.Equals(expectedCode, response.code);
               Assert.Equals(expectedData, response.body);
          }

          [Fact]
          public void apiDoesntServeToInvalidToken()
          {
               string token = "";
               int expectedCode = 401;
               string expectedData = "";
               Assert.Equals(expectedCode, response.code);
               Assert.Null(response.body);
               Assert.Equals(expectedData, response.body);
          }
     }  //class
}  //namespace
