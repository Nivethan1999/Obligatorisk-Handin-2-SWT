namespace ChargingStation.lib.Interfaces;

public class RfidEventArgs : EventArgs
{
     public int ID { get; set; }
}

public interface IRfidReader
{
     event EventHandler<RfidEventArgs> RfidEvent;
     void OnRfidDetected(int id);
}