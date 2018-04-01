using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jwtsoo.Pages
{
     public class IndexModel : PageModel
     {
          [BindProperty]
          public string Username { get; set; }
          [BindProperty]
          public string Password { get; set; }
          [BindProperty]
          public string AuthStatus { get; set; }
          [BindProperty]
          public string Token { get; set; }
          [BindProperty]
          public string DataStatus { get; set; }
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
               if (!ModelState.IsValid)
               {
                    return Page();
               }
               string authUri = _configuration["AuthAPI"];
               TokenRequest request = new TokenRequest(Username, Password);
               string tokenRequestString = JsonConvert.SerializeObject(request);
               HttpContent requestContent = new StringContent(tokenRequestString, Encoding.UTF8, "application/json");
               try
               {
                    HttpResponseMessage response = await client.PostAsync(authUri, requestContent);
                    AuthStatus = response.StatusCode.ToString();
                    Token = await response.Content.ReadAsStringAsync();
                    Message = $" JWT obtained at { DateTime.Now }";
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
               if (!ModelState.IsValid)
               {
                    return Page();
               }
               string dataUri = _configuration["DataAPI"];
               client.DefaultRequestHeaders.Clear();
               client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
               try
               {
                    HttpResponseMessage response = await client.GetAsync(dataUri);
                    DataStatus = response.StatusCode.ToString();
                    Data = await response.Content.ReadAsStringAsync();
                    Message = $" Data obtained at { DateTime.Now }";
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
          public TokenRequest(string u, string p)
          {
               Username = u;
               Password = p;
          }  //ctor
     }  //TokenRequest class
}  //namespace
