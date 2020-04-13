using Microsoft.Extensions.Logging;
using MontyHall.Models;
using MontyHall.Models.DTO;
using MontyHall.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyHall.Services
{
    public class GameSimulatorService : IGameSimulatorService
    {
        private readonly IMontyHallService svc;
        private readonly ILogger<GameSimulatorService> logger;

        public GameSimulatorService(IMontyHallService svc, ILogger<GameSimulatorService> logger)
        {
            this.svc = svc;
            this.logger = logger;
        }

        public async Task<IEnumerable<GameResult>> SimulateAsync(int noOfSimulations, KeepDoorOption keepDoorOption, CancellationToken token)
        {
            try
            {
                var games = Enumerable.Range(0, noOfSimulations).Select(x => { return new Game() {index = x }; }).ToList();
                var gamesTasks = games.AsParallel().WithCancellation(token).Select(x => { return ExecuteAsync(x, keepDoorOption, token); });
                return await Task.WhenAll(gamesTasks.ToList()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        protected virtual async Task<GameResult> ExecuteAsync(Game game, KeepDoorOption keepDoorOption, CancellationToken token)
        {
            if (game == null)
                throw new System.Exception(ErrorMessages.InvalidGameObject);            

            if (token.IsCancellationRequested)
                token.ThrowIfCancellationRequested();            

            //Step 1: User choose first door
            int randomDoorIndex;
            int selectedDoorIndex;
            selectedDoorIndex = new System.Random().Next(game.Doors.Length);
            randomDoorIndex = svc.ChooseFirstDoor(game, selectedDoorIndex);                              

            //Step 2: User does not keep door 
            if (keepDoorOption == KeepDoorOption.NotKeepDoor)
            {
                var execludedDoorIndex = new List<int>() { selectedDoorIndex, randomDoorIndex };
                selectedDoorIndex = Enumerable.Range(0, 3).Except(execludedDoorIndex).First();
            }
            
            svc.ChooseSecondDoor(game, selectedDoorIndex);            
            
            return svc.GetGameResult(game);            
        }

    }

}
