using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.Text;

namespace authAPI.Controllers
{
     [Route("api/authorization")]
     public class AuthController : Controller
     {
          private static IConfiguration _configuration;
          private static string _authorizedUserName;
          private static string _authorizedPassword;

          public AuthController()
          {
               _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          }  //ctor

          [AllowAnonymous]
          [HttpPost]
          [Route("RequestToken")]
          public IActionResult RequestToken([FromBody] TokenRequest request)
          {
               _authorizedUserName = _configuration["AuthorizedUserName"];
               _authorizedPassword = _configuration["AuthorizedPassword"];
               if (request.Username == _authorizedUserName && request.Password == _authorizedPassword)
               {
                    Claim[] claims = new[]
                    {
                         new Claim(ClaimTypes.Name, request.Username)
                    };
                    string token = GenerateJWT(claims, DateTime.Now.AddMinutes(30));
                    return Ok(token);
               }
               else
               {
                    return Unauthorized();
               }
          }  //RequestToken

          public string GenerateJWT(Claim[] claims, DateTime expiration)
          {
               SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
               SigningCredentials signingCreds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
               JwtSecurityToken token = new JwtSecurityToken(
                 issuer: "cscarter.net",
                 audience: "cscarter.net",
                 claims: claims,
                 expires: expiration,
                 signingCredentials: signingCreds);
               return new JwtSecurityTokenHandler().WriteToken(token);
          }  //GenerateJWT
     }  //AuthController class

     public class TokenRequest
     {
          public string Username { get; set; }
          public string Password { get; set; }
     }  //TokenRequest class
}  //namespace
