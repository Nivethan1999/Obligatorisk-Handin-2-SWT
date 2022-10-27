using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using UsbSimulator;

namespace ChargingStation.test;

public class ChargeControl : IChargeControl
{
    public IUsbCharger _charger;
    public IDisplay _display;
    public double lastCurrent { get; private set; }
    public bool Connected { get; }
    
    public ChargeControl(IDisplay display, IUsbCharger charger)
    {
        _charger = charger;
        _display = display;
        _charger.CurrentValueEvent += OnNewCurrent;

    }
    public void StartCharge()
    {
        _charger.StartCharge();
    }

    public void StopCharge()
    {
        _charger.StopCharge();
    }
    
    public void OnNewCurrent(object sender, CurrentEventArgs e)
    {
        if (e.Current == lastCurrent) return;
        lastCurrent = e.Current;
        
        switch (e.Current)
        {   
            case 0.0:
                _display.ConnectPhone();
                break;
            case > 0.0 and <= 5.0:
                _display.RemovePhone();
                break;
            case > 5.0 and <= 500.0:
                Console.WriteLine("Phone is charging");
                _display.PhoneConnected();
                break;
            case > 500.0:
                _display.ChargeError();
                StopCharge();
                break;
        }
    }
    
    
    
    
}