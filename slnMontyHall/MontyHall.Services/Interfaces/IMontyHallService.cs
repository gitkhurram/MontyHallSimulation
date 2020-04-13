using MontyHall.Models;
using MontyHall.Models.DTO;

namespace MontyHall.Services.Interfaces
{
    public interface IMontyHallService
    {
        public int ChooseFirstDoor(Game game, int doorIndex);
        public void ChooseSecondDoor(Game game, int selectDoorIndex, bool openAllDoors = true);        
        public GameResult GetGameResult(Game game);
    }
}
