using ChargingStation.lib;

namespace ChargingStation.test;

public class ChargeControl : IChargeControl
{
    
    public bool Connected { set; get; }
    
    public ChargeControl()
    {
        Connected = false;
    }
    public void StartCharge()
    {
        throw new NotImplementedException();
    }

    public void StopCharge()
    {
        throw new NotImplementedException();
    }
}