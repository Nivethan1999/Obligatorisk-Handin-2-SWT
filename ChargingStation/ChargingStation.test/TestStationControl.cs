using System.Runtime.InteropServices;
using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;
using NSubstitute;
using UsbSimulator;
using static ChargingStation.StationControl.ChargingStationState;

namespace ChargingStation.test;

public class TestStationControl
{
    private ChargeControl _charge;
    private Display _display;
    private IUsbCharger _usbCharger;
    private StationControl _uut;
    private Door _door;
    private RfidReader _rfidReader;
    private LogFile _logFile;


    [SetUp]
    public void Setup()
    {
        _display = Substitute.For<Display>();
        _usbCharger = Substitute.For<IUsbCharger>();
        _door = Substitute.For<Door>();
        _logFile = Substitute.For<LogFile>("Test.txt");
        _charge = Substitute.For<ChargeControl>(_display, _usbCharger);
        _rfidReader = Substitute.For<RfidReader>();
        _uut = new StationControl(_rfidReader, _charge, _door, _display, _logFile);
    }

    [Test]
    public void TestOnDoorOpenedStateAvailible()
    {
        _uut._state = Available;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = true });
        Assert.That(_uut._state, Is.EqualTo(DoorOpen));
    }
    
    [Test]
    public void TestOnDoorOpenedStateLocked()
    {
        _uut._state = Locked;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = true });
        Assert.That(_uut._state, Is.EqualTo(Locked));
    }
    
    [Test]
    public void TestOnDoorOpenedStateDoorOpen()
    {
        _uut._state = DoorOpen;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = true });
        Assert.That(_uut._state, Is.EqualTo(DoorOpen));
    }
    
    [TestCase(Available)]
    [TestCase(Locked)]
    [TestCase(DoorOpen)]
    public void TestOnDoorOpenedArgFalse(StationControl.ChargingStationState state)
    {
        _uut._state = state;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = false });
        Assert.That(_uut._state, Is.EqualTo(state));
    }
    
    [Test]
    public void TestOnDoorOpenedStateInvalid()
    {
        
    }

    [Test]
    public void TestOnDoorClosedAvailable()
    {
        _uut._state = Available;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = false });
        Assert.That(_uut._state, Is.EqualTo(Available));

      

    }

    [Test]
    public void TestOnDoorClosedLocked()
    {
        _uut._state = Locked;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = false });
        Assert.That(_uut._state, Is.EqualTo(Locked));

    }

}