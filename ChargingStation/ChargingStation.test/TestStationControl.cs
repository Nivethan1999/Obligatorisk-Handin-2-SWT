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
    public void TestOnDoorOpened()
    {
        _uut._state = Available;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = true });
        Assert.That(_uut._state, Is.EqualTo(DoorOpen));
    }
}