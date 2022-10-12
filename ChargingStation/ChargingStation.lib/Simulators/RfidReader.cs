using ChargingStation.lib.Interfaces;

namespace ChargingStation.lib.Simulators;

public class RfidReader : IRfidReader
{
    public void OnRfidRead(int id)
    {
        throw new NotImplementedException();
    }

    public double CurrentValue { get; }
    public void RfidDetected(int Id)
    {
        throw new NotImplementedException();
    }

    public bool ReadRfid()
    {
        throw new NotImplementedException();
    }

    public event EventHandler<RFIDDetectedEvent>? DetectedEvent;
}
    