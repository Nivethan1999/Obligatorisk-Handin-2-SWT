using ChargingStation.lib.Interfaces;

namespace ChargingStation.lib.Simulators;

public class RfidReader : IRfidReader
{

    public event EventHandler<RfidEventArgs>? RfidEvent;

     public void OnRfidDetected(int id)
     {
         RfidEvent?.Invoke(this, new RfidEventArgs {ID = id});
     }
}