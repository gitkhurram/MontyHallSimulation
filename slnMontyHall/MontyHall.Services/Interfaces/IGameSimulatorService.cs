using MontyHall.Models.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MontyHall.Services.Interfaces
{
    public interface IGameSimulatorService
    {
        Task<IEnumerable<GameResult>> SimulateAsync(int noOfSimulations, KeepDoorOption keepDoorOption, CancellationToken token);
    }
}