using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CalculateController> _logger;

        public CalculateController(ILogger<CalculateController> logger)
        {
            _logger = logger;
        }

        // 9. Now we can add implementation to this method (after implementing functionality using TDD).
        [HttpGet(Name = "Add/{left}/{right}")]
        public int Get(int left, int right)
        {
            //throw new NotImplementedException();
            //var calculator = new Calculator();
            //return calculator.Sum(left, right);

            // 9. implementation.
            return new Calculator().Sum(left, right);
        }
    }
}