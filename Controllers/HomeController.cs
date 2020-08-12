using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiddlewareWinAuth.Models;
using MiddlewareWinAuth.Services;
using static MiddlewareWinAuth.Models.dto;

namespace MiddlewareWinAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[HttpGet]
        //[Route("token")]
        //public async Task<TokenDetail> Token()
        //{
        //    ProcessToken pt = new ProcessToken();
        //    TokenDetail tk = new TokenDetail();
        //    try
        //    {

        //       // var user = (WindowsIdentity)HttpContext.User.Identity;
        //        var curUser = System.Security.Principal.WindowsIdentity.GetCurrent();


        //        tk.name = curUser.Name;
        //        tk.token = curUser.Token.ToString();
        //        tk.AccessToken = curUser.AccessToken.DangerousGetHandle().ToString();
        //        tk.name2 = curUser.Name;
        //    }
        //    catch (Exception ex)
        //    {
        //        tk.error = ex.Message + "\n\r\n\r" + ex.StackTrace;
        //    }
        //    return tk;
        //}
        [HttpGet]
        [Route("authtoken")]
        public async Task<TokenDetail> Token()
        {
            ProcessToken pt = new ProcessToken();
            TokenDetail tk = new TokenDetail();
            try
            {

                //var user= ServerCertificate


               
                tk.name = WindowsIdentity.GetCurrent().Name;
                tk.token =  WindowsPrincipal.Current.Identity.Name;
                //tk.AccessToken = curUser.AccessToken.DangerousGetHandle().ToString();
                //tk.name2 = curUser.Name;
            }
            catch (Exception ex)
            {
                tk.error = ex.Message + "\n\r\n\r" + ex.StackTrace;
            }
            return tk;
        }
    }
}
