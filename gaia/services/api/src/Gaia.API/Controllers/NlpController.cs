using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gaia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NlpController : ControllerBase
    {
        private readonly ILogger<NlpController> _logger;

        public NlpController(ILogger<NlpController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("NLP!");
        }
    }
}
