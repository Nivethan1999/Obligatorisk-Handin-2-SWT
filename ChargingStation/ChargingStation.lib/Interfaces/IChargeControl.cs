using UsbSimulator;

namespace ChargingStation.lib;

public interface IChargeControl
{
    public bool Connected { get; set; }
    public void StartCharge();
    public void StopCharge();
}