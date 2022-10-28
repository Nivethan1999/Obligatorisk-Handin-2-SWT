using System.Runtime.InteropServices;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;
using NSubstitute;
using UsbSimulator;

namespace ChargingStation.test;

public class TestChargeControl
{
    private ChargeControl _uut;
    private Display _display;
    private IUsbCharger _usbCharger;
    
    [SetUp]
    public void Setup()
    {
        _display = Substitute.For<Display>();
        _usbCharger = Substitute.For<IUsbCharger>();
        _uut = new ChargeControl(_display,_usbCharger);
        _usbCharger.CurrentValueEvent += (o, args) => _uut.OnNewCurrent(o,args);
    }

    [Test]
    public void TestStartCharge()
    {
        _uut.StartCharge();
        _usbCharger.Received(1).StartCharge();
    }

    [Test]
     public void TestNormalCharge()
     {
          _uut._UsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 500 });
          Assert.That(_uut.lastCurrent, Is.EqualTo(500));

     }

     [Test]
     public void TestNoCharge()
     {
         // Sets the state previous to the test to "Charging at 500 mA"
         _uut._UsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 500 });
          double value = 0.0;
          
          _uut._UsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = value });
          _display.Received(1).ConnectPhone();
          
          Assert.That(_uut.lastCurrent, Is.EqualTo(value));
     }

     [Test]
     public void TestFullyCharge()
     {
          var value = 3;
          _uut._UsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = value });
         
          Assert.That(_uut.lastCurrent, Is.EqualTo(value));
     }

     [Test]
    public void TestStopCharge()
    {
         var value = 501;
          _uut._UsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = value });
         
         Assert.That(_uut.lastCurrent, Is.EqualTo(value));
     }
    
    [Test]
    public void TestOverloadCurrent()
    {
        _uut._UsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = 750});
        _usbCharger.Received(1).StopCharge();
        Assert.That(_uut._UsbCharger.CurrentValue, Is.EqualTo(0));
    }
}