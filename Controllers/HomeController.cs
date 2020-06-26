using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using opg_201910_interview.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using opg_201910_interview.Comparers;

namespace opg_201910_interview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            string[] clientFilesA = Directory.GetFiles(_config.GetValue<string>("ClientSettings:ClientA:FileDirectoryPath"));
            string[] clientFilesB = Directory.GetFiles(_config.GetValue<string>("ClientSettings:ClientB:FileDirectoryPath"));

            List<string> sortedA = clientFilesA.ToList();
            List<string> sortedB = clientFilesB.ToList();
            Regex nameFormatA = new Regex("[a-zA-Z]+-\\d{4}-\\d{2}-\\d{2}");
            Regex nameFormatB = new Regex("[a-zA-Z]+_\\d{8}");

            sortedA.RemoveAll(x => !nameFormatA.IsMatch(x));
            sortedB.RemoveAll(x => !nameFormatB.IsMatch(x));

            sortedA.Sort(new CompareA());
            sortedB.Sort(new CompareB());

            JsonResult result = new JsonResult(new { sortedA, sortedB });
            return View(result);
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
