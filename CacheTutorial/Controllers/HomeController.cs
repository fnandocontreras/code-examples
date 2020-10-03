using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CacheTutorial.Models;
using CacheTutorial.Services;
using System.Threading;

namespace CacheTutorial.Controllers
{
    public class HomeController : Controller
    {
        ICacheServiceAsync _cache;
        
        public HomeController(ICacheServiceAsync cache)
        {
            _cache = cache;
        }

        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            var helloKey = new CacheKeySettings { Key = "hello_message", Expiry = TimeSpan.FromHours(1) };

            ViewBag.Message = await _cache.GetAsync<string>(helloKey, ()=> GetHelloAsync(), cancellation);
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

        private async Task<string> GetHelloAsync()
        {
            await Task.Delay(3000);
            return "Hello World!";
        }
    }
}
