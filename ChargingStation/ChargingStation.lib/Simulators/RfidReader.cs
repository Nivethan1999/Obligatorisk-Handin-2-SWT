using ChargingStation.lib.Interfaces;

namespace ChargingStation.lib.Simulators;

public class RfidReader : IRfidReader
{
     public event EventHandler<RFIDDetectedEvent>? DetectedEvent;
     public void OnRfidRead(int id)
     {
         throw new NotImplementedException();
     }

     public void ReadRfid(int id)
     {
          DetectedEvent?.Invoke(this,new RFIDDetectedEvent{ID = id});
     }
}