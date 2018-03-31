using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace authAPI.Controllers
{
     [Route("api/[controller]")]
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

                    SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                    SigningCredentials creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: "cscarter.net",
                        audience: "cscarter.net",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(tokenString);
               }

               return BadRequest("Could not verify username and password");
          }
     }  //AuthController class

     public class TokenRequest
     {
          public string Username { get; set; }
          public string Password { get; set; }
     }  //TokenRequest class
}  //namespace
