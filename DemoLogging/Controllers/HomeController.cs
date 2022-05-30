using DemoLogging.Data;
using DemoLogging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLogging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _logger.LogInformation("NLog injected into HomeController");
            _db = db;
            _logger.LogInformation("Use Database");
            _clientFactory = clientFactory;
            _logger.LogInformation("HttpClientFactory injected into HomeController");
            _configuration = configuration;
            _logger.LogInformation("Iconfiguration injected into HomeController");

        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello, this is the index!");

            HttpClient client = _clientFactory.CreateClient();
            string url = _configuration.GetValue<string>("Configuration:UrlApi");
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("/todos").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            List<TodoModel> allItems = JsonConvert.DeserializeObject<List<TodoModel>>(jsonData);

            return View(allItems);
        }

        public async Task Save(int id)
        {
            _logger.LogInformation("Start save method");
            TodoService todo = new TodoService();
            var model = await todo.GetTodo(id);
            await _db.Todos.AddAsync(
                new Todo
                {
                    Id = model.Id,
                    Title = model.Title,
                    Completed = model.Completed
                });
            await _db.SaveChangesAsync();
            _logger.LogInformation("todo has been saved");
            RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Hello, this is the Privacy!");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Hello, this is the Error!");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
