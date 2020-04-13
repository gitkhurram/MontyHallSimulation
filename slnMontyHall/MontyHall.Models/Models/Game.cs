using MontyHall.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHall.Models
{
    public enum GameState
    { 
        Initial = 0,
        FirstDoorSelected = 1,
        Completed = 2
    }

    public class Game
    {
        public int index { get; set; }
        public GameState State { get; set; } = GameState.Initial;
        public int CarDoorIndex { get; }
        public int SelectedDoorIndex { get; set; } = -1;
        public Door[] Doors { get; set; }
        public Game()
        {
            this.Doors = Enumerable.Range(0, 3).Select(x => new Door() { Index = x }).ToArray();
            this.CarDoorIndex = new System.Random((int)System.DateTime.Now.Ticks).Next(0, 3);            
            Doors[CarDoorIndex].Value = enmDoorValue.Car;
        }
    }
}
