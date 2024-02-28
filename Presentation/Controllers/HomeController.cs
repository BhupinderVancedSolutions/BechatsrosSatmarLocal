using Application.Authentication.Queries;
using DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Presentation.Authorization;
using Presentation.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [CustomAuthorization]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> TeamConnectTone([FromBody] TeamConnectToneRequest teamConnectToneRequest)
        {
            try
            {
                return Json("");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
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
    }
}
