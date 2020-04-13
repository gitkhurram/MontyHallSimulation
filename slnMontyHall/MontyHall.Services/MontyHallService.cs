using MontyHall.Models;
using MontyHall.Models.DTO;
using MontyHall.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MontyHall.Services
{
    public class MontyHallService : IMontyHallService
    {
        public virtual int ChooseFirstDoor(Game game, int doorIndex)
        {
            if (game == null)
                throw new System.Exception(ErrorMessages.InvalidGameObject);

            if (game.State != GameState.Initial)
                throw new System.Exception(ErrorMessages.InvalidGameStep);
            
            //Set selected door of game
            game.SelectedDoorIndex = doorIndex;

            //system opens the random door (goat and non-selected)            
            var SysDoor = game.Doors.Where(x => x.Value == enmDoorValue.Goat && x.Index != game.SelectedDoorIndex).First<Door>();
            SysDoor.State = enmDoorState.Open;
            var randomDoorIndex = SysDoor.Index;            

            //set the game state
            game.State = GameState.FirstDoorSelected;

            return randomDoorIndex;
        }        

        public virtual void ChooseSecondDoor(Game game, int selectDoorIndex, bool openAllDoors = true)
        {
            if (game == null)
                throw new System.Exception(ErrorMessages.InvalidGameObject);

            if (game.State != GameState.FirstDoorSelected)
                throw new System.Exception(ErrorMessages.InvalidGameStep);

            
            if (selectDoorIndex != game.SelectedDoorIndex)
            {
                var openedDoorIndex = game.Doors.Where(x => x.State == enmDoorState.Open).First().Index;
                var execludedDoorIndex = new List<int>() { game.SelectedDoorIndex, openedDoorIndex };
                game.SelectedDoorIndex = Enumerable.Range(0, 3).Except(execludedDoorIndex).First();
            }

            //System opens all the doors
            foreach (Door door in game.Doors)
                door.State = enmDoorState.Open;

            //set the game state
            game.State = GameState.Completed;

            return;
        }

        public virtual GameResult GetGameResult(Game game)
        {
            if (game == null)
                throw new System.Exception(ErrorMessages.InvalidGameObject);

            if (game.SelectedDoorIndex == game.CarDoorIndex)
                return GameResult.Win;

            return GameResult.Loose;
        }

    }
}
