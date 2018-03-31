using System;
using Xunit;
using authAPI;

namespace JWTauthTests
{
    public class AuthTests
    {
        [Fact]
        public void apiServesTokenToAuthorized()
        {
               string username = "admin";
               string password = "secret";
               int expectedCode = 200;
               string expectedData = "";
               Assert.Equals(expectedCode, response.code);
               Assert.Equals(expectedData, response.body);
          }

          [Fact]
          public void apiDoesntServeTokenToUnauthorized()
          {
               string username = "admin";
               string password = "wrong";
               int expectedCode = 200;
               string expectedData = "";
               Assert.Equals(expectedCode, response.code);
               Assert.Null(response.body);
               Assert.Equals(expectedData, response.body);
          }
     }  //class
}  //namespace
