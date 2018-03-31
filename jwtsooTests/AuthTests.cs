using System;
using Xunit;
using authAPI;

namespace jwtsooTests
{
    public class AuthTests
    {
        [Fact]
        public void apiServesTokenToAuthorized()
        {
               string username = "admin";
               string password = "secret";
               int expectedCode = 200;
               string expectedToken = "";
               Assert.Equals(expectedCode, response.code);
               Assert.Equals(expectedToken, response.body);
          }

          [Fact]
          public void apiDoesntServeTokenToUnauthorized()
          {
               string username = "admin";
               string password = "wrong";
               int expectedCode = 401;
               string expectedToken = "";
               Assert.Equals(expectedCode, response.code);
               Assert.Null(response.body);
               Assert.Equals(expectedToken, response.body);
          }
     }  //class
}  //namespace
