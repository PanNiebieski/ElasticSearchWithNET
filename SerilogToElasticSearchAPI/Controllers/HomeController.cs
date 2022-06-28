using Microsoft.AspNetCore.Mvc;

namespace SerilogToElasticSearchAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/Index")]
        public string Index()
        {
            _logger.LogInformation("HomeController Index executed at {date}", DateTime.UtcNow);

            return "(Hello)";
        }

        [HttpGet("/Error1")]
        public int Error1()
        {
            try
            {
                throw new Exception("Some bad code was executed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred on the Error1");
            }

            return 500;
        }

        [HttpGet("/Error2")]
        public int Error2()
        {
            try
            {
                throw new NullReferenceException("Dupa was null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred on the Error2");
            }

            return 400;
        }


        [HttpGet("/Error3")]
        public int Error3()
        {
            try
            {
                throw new BadHttpRequestException("Bad Request");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unknown error occurred on the Error3");
            }

            return 400;
        }
    }
}