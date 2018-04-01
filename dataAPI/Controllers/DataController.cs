using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace dataAPI.Controllers
{
     [Route("api/data")]
     public class DataController : Controller
     {
          private static IConfiguration _configuration;

          public DataController()
          {
               _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          }  //ctor

          [HttpGet]
          //[Authorize(Policy = "Admin")]
          [Authorize]
          [Route("GetData")]
          public IActionResult GetData()
          {
               return Ok($"This is your data as of {DateTime.Now}");
          }  //GetData
     }  //class
}  //namespace
