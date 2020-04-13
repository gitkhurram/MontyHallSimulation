using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MontyHall.Models.DTO;
using MontyHall.Services.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyHall.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MontyHallController : ControllerBase
    {
        private readonly ILogger<MontyHallController> _logger;
        private readonly IGameSimulatorService svc;        
        public MontyHallController(ILogger<MontyHallController> logger, IGameSimulatorService svc)
        {
            _logger = logger;
            this.svc = svc;
        }

        [HttpGet]
        [Route("Simulate")]        
        public async Task<IActionResult> SimulateAsync([FromQuery] uint noOfSimulations, [FromQuery] KeepDoorOption keepDoorOption, CancellationToken token)
        {
            _logger.LogInformation($"Simulate started Total: {noOfSimulations}");
            
            var results = await svc.SimulateAsync((int)noOfSimulations, keepDoorOption, token);

            _logger.LogInformation($"Simulate completed Total: {noOfSimulations}");

            var summary = new GamesResultSummary
            {
                TotalWin = results.Count(x => x == GameResult.Win),
                TotalLoose = results.Count(x => x == GameResult.Loose)
            };
            
            return new ObjectResult(summary);
        }

    }
}
