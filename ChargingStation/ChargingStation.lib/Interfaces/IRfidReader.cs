namespace ChargingStation.lib.Interfaces;


public class RFIDDetectedEvent : EventArgs
{
     public double DetectedEvent { get; set; }
}

public interface IRfidReader
{
     double CurrentValue { get; }
     public void RfidDetected(int Id);

     public bool ReadRfid();

     event EventHandler<RFIDDetectedEvent> DetectedEvent;
     void OnRfidRead(int id);
}