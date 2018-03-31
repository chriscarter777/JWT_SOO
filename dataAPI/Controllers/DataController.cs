using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dataAPI.Controllers
{
     [Route("api/data")]
     public class DataController : Controller
     {
          [HttpGet]
          [Authorize(Policy = "Admin")]
          [Route("GetData")]
          public string GetData()
          {
               Debug.WriteLine(Request.Headers["Authorization"]);
               return $"This is your data as of {DateTime.Now}";
          }  //GetData
     }  //class
}  //namespace
