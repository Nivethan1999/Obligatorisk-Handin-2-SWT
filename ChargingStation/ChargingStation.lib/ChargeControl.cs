using ChargingStation.lib;
using UsbSimulator;

namespace ChargingStation.test;

public class ChargeControl : IChargeControl
{
    private IUsbCharger _charger;
    public bool Connected { get;}
    
    public ChargeControl()
    {
        
        _charger = new UsbChargerSimulator();
        Connected = _charger.Connected;
    }
    public void StartCharge()
    {
        _charger.StartCharge();
    }

    public void StopCharge()
    {
        _charger.StopCharge();
    }
    
    
    
    
}