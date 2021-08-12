using EntityContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using WordCounter.Models;

namespace WordCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TestContext _Context = null;
        public HomeController(ILogger<HomeController> logger, TestContext context)
        {
            _logger = logger;
            _Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult WordsList(string txtar)
        {
            List<Word> res = new List<Word>();
            if (string.IsNullOrWhiteSpace(txtar)) return PartialView(res);
            using (BusinessLogic.WordsBusiness action = new BusinessLogic.WordsBusiness(_Context))
            {
                action.AddToDb(HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress.ToString() : "", txtar);
                res = action.GetTable();
            }
            return PartialView(res);
        }
    }
}
