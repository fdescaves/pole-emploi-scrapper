using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoleEmploiScrapper.Application.Models;
using PoleEmploiScrapper.Application.Services;

namespace PoleEmploiScrapper.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly ILogger<ReportingController> _logger;
        private readonly IReportingService _reportingService;

        public ReportingController(ILogger<ReportingController> logger, IReportingService reportingService)
        {
            _logger = logger;
            _reportingService = reportingService;
        }

        /// <summary>
        /// Get statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            JobOfferStatisticsDto statistics = await _reportingService.GetStatistics();
            return Ok(statistics);
        }
    }
}
