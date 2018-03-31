using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace jwtsoo.Pages
{
    public class IndexModel : PageModel
    {
          public string Username { get; set; }
          public string Password { get; set; }
          public int? AuthResponseCode { get; set; }
          public string Token { get; set; }
          public int? DataResponseCode { get; set; }
          public string Data { get; set; }

          public void OnGet()
        {

        }
    }  //IndexModel class

     public class TokenRequest
     {
          public string Username { get; set; }
          public string Password { get; set; }
     }  //TokenRequest class
}  //namespace
