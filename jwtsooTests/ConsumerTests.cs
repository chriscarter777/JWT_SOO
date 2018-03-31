using System;
using Xunit;
using jwtsoo;

namespace JWTauthTests
{
     public class ConsumerTests
     {
          [Fact]
          public void ConsumerMakesAuthRequest()
          {
               string token = "";
               int expectedCode = 200;
               string expectedData = "This is your data.";
               Assert.Equals(expectedCode, response.code);
               Assert.Equals(expectedData, response.body);
          }

          [Fact]
          public void ConsumerMakesDataRequest()
          {
               string token = "";
               int expectedCode = 200;
               string expectedData = "This is your data.";
               Assert.Equals(expectedCode, response.code);
               Assert.Equals(expectedData, response.body);
          }
     }  //class
}  //namspace