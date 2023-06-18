using CommonLibrary.Models;

using Microsoft.AspNetCore.Mvc;

using PurchaseAPI.Contexts;

namespace PurchaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private PurchaseDBContext db;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,PurchaseDBContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Purchase> Get()
        {
            return db.Purchases;
        }
    }
}