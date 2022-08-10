using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynelProject.Models;
using SynelProject.Data.DBContexts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SynelProject.Domain.Models;
using SynelProject.Service;

namespace SynelProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly SynelDbContext _dbcontext;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, 
            SynelDbContext dbContext, IEmployeeService employeeService)
        {
            _logger = logger;
            _environment = environment;
            _dbcontext = dbContext;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("store-to-database")]
       public async Task<IActionResult> ConvertAsync([FromForm] IFormFile file)
        {
            if (file == null)
                return View("Index", new List<Employee>());

            string rootPath = _environment.WebRootPath;

            //save to resources
            await _employeeService.SaveFileAsync(file, rootPath);

            //extract to grid
            var users = _employeeService.CsvToCollection(file);

            //store to database
            await _dbcontext.Employees.AddRangeAsync(users);
            await _dbcontext.SaveChangesAsync();

            return View("Index", users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
