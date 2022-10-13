namespace ChargingStation.lib.Interfaces;

public class RFIDDetectedEvent : EventArgs
{
     public int ID { get; set; }
}

public interface IRfidReader
{
     event EventHandler<RFIDDetectedEvent> DetectedEvent;
}