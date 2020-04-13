namespace MontyHall.Models
{
    public enum enmDoorValue
    {
        Car = 0,
        Goat = 1
    }

    public enum enmDoorState
    { 
        Closed = 0,
        Open = 1
    }

    public class Door
    {   
        public int Index { get; set; }
        public enmDoorValue Value { get; set; } = enmDoorValue.Goat;        
        public enmDoorState State { get; set; } = enmDoorState.Closed;
    }
}
