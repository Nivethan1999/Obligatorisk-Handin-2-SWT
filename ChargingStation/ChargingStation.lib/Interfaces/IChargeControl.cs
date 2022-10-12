namespace ChargingStation.lib;

public interface IChargeControl
{
    public bool Connected { get; }
    
    public void StartCharge();
    public void StopCharge();
}