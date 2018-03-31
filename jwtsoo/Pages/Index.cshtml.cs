using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace jwtsoo.Pages
{
     public class IndexModel : PageModel
     {
          [BindProperty]
          public string Username { get; set; }
          [BindProperty]
          public string Password { get; set; }
          [BindProperty]
          public string Token { get; set; }
          [BindProperty]
          public string Data { get; set; }
          [BindProperty]
          public string Message { get; set; }
          private IConfiguration _configuration;
          private static readonly HttpClient client = new HttpClient();

          public IndexModel()
          {
               _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          }  //ctor

          public void OnGet()
          {
               Message = $" Server time is { DateTime.Now }";
          }

          public async Task<IActionResult> OnPostGetTokenAsync()
          {
               Message = $" Server time is { DateTime.Now }";
               if (!ModelState.IsValid)
               {
                    return Page();
               }
               string authUrl = _configuration["AuthAPI"];
               TokenRequest request = new TokenRequest { Username = Username, Password = Password };
               string tokenRequestString = JsonConvert.SerializeObject(request);
               HttpContent requestContent = new StringContent(tokenRequestString, Encoding.UTF8, "application/json");
               try
               {
                    HttpResponseMessage response = await client.PostAsync(authUrl, requestContent);
                    string responseString = await response.Content.ReadAsStringAsync();
                    Token = String.IsNullOrEmpty(responseString) ? "Invalid Request" : responseString;
                    return Page();
               }
               catch (HttpRequestException e)
               {
                    Message = e.Message;
                    return Page();
               }
          }  //GetToken

          public async Task<IActionResult> OnPostGetDataAsync()
          {
               Message = $" Server time is { DateTime.Now }";
               if (!ModelState.IsValid)
               {
                    return Page();
               }
               string dataUrl = _configuration["DataAPI"];
               client.DefaultRequestHeaders.Clear();
               client.DefaultRequestHeaders.Add("Authorization", Token);
               try
               {
                    string responseString = await client.GetStringAsync(dataUrl);
                    Data = String.IsNullOrEmpty(responseString) ? "Invalid Request" : responseString;
                    return Page();
               }
               catch (HttpRequestException e)
               {
                    Message = e.Message;
                    return Page();
               }
          }  //GetData
     }  //IndexModel class

     public class TokenRequest
     {
          public string Username { get; set; }
          public string Password { get; set; }
     }  //TokenRequest class
}  //namespace
