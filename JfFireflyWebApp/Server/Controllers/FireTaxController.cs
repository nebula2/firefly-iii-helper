using JfFireflyWebApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JfFireflyWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FireTaxController : ControllerBase
    {
        private readonly ILogger<FireTaxController> _logger;
        private readonly IFireTaxService _fireTaxService;

        public FireTaxController(IFireTaxService fireTaxService, ILogger<FireTaxController> logger)
        {
            _fireTaxService = fireTaxService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Not implemented yet");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSystemInfo()
        {
            return Ok(await _fireTaxService.GetSystemInfoAsync());
        }

        [HttpGet("[action]/{startDate}/{endDate}")]
        public async Task<IActionResult> GetFromDateRange(string startDate, string endDate)
        {
            _logger.LogInformation("GetFromDateRange start.");
            try
            {
                var start = DateTimeOffset.Parse(startDate);
                var end = DateTimeOffset.Parse(endDate);
                return Ok(await _fireTaxService.GetTaxmanResultAsync(start, end));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFromDateRange ERROR");
                return BadRequest(ex);
            }
        }
    }
}
