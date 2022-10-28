using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using UsbSimulator;

namespace ChargingStation.test;

public class ChargeControl : IChargeControl
{
    public IUsbCharger _charger;
    public IDisplay _display;
    public double lastCurrent { get; private set; }
    public bool Connected { get; private set; }
    public enum State { Charging, NotCharging, FinishedCharging, Error }
    public State _lastState = State.NotCharging;
    
    public ChargeControl(IDisplay display, IUsbCharger charger)
    {
        _charger = charger;
        _display = display;
        _charger.CurrentValueEvent += OnNewCurrent;
        Connected = _charger.Connected;
        lastCurrent = _charger.CurrentValue;

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
        Connected = _charger.Connected;
        
        switch (e.Current)
        {   
            case 0.0:
                if (_lastState == State.NotCharging) return;
                _display.ConnectPhone();
                _lastState = State.NotCharging;
                break;
            case > 0.0 and <= 5.0:
                if (_lastState == State.FinishedCharging) return;
                _display.RemovePhone();
                _lastState = State.FinishedCharging;
                break;
            case > 5.0 and <= 500.0:
                if (_lastState == State.Charging) return;
                _display.PhoneConnected();
                _lastState = State.Charging;
                break;
            case > 500.0:
                if (_lastState == State.Error) return;
                _display.ChargeError();
                _lastState = State.Error;
                StopCharge();
                break;
        }
    }
    
    
    
    
}