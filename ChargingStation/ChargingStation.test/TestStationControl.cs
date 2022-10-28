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
        _usbCharger = Substitute.For<UsbChargerSimulator>();
        _door = Substitute.For<Door>();
        _logFile = Substitute.For<LogFile>("Test.txt");
        _charge = Substitute.For<ChargeControl>(_display, _usbCharger);
        _rfidReader = Substitute.For<RfidReader>();
        _uut = new StationControl(_rfidReader, _charge, _door, _display, _logFile);
    }

    
    // TEST ON RFID DETECTED
    [Test]
    public void TestOnRfidDetected_Locked_IdIsCorrect()
    {
        _uut._state = Locked;
        _uut._oldId = 123456;
        _uut.OnRfidDetected(this, new RfidEventArgs() {ID = 123456});
        
        _uut._charger.Received(1).StopCharge();
        _uut._door.Received(1).UnlockDoor();
        _uut._logFile.Received(1).WriteLogEntry("Skab låst op med RFID", 123456);

        _uut._display.Received(1).RemovePhone();
        Assert.That(_uut._state, Is.EqualTo(Available));
    }
    
    [Test]
    public void TestOnRfidDetected_Locked_IdIsWrong()
    {
        _uut._state = Locked;
        _uut._oldId = 456;
        _uut.OnRfidDetected(this, new RfidEventArgs() {ID = 123456});
        _uut._display.Received(1).RFIDError();
        Assert.That(_uut._state, Is.EqualTo(Locked));
    }
    
    [Test]
    public void TestOnRfidDetected_DoorOpen()
    {
        _uut._state = DoorOpen;
        _uut._oldId = 123456;
        _uut.OnRfidDetected(this, new RfidEventArgs() {ID = 123456});
        Assert.That(_uut._state, Is.EqualTo(DoorOpen));
    }
    
    [Test] 
    public void TestOnRfidDetected_Available_PhoneConnected()
    {
        RfidEventArgs rfidEventArgs = new RfidEventArgs() {ID = 123456};
        _uut._charger.Connected=true;
        _uut._state = Available;
        _uut.OnRfidDetected(this, rfidEventArgs);
        _uut._door.Received(1).LockDoor();
        _uut._charger.Received(1).StartCharge();
        _uut._logFile.Received(1).WriteLogEntry("Skab låst med RFID", rfidEventArgs.ID);
        _uut._display.Received(1).Occupied();
        _uut._display.Received(1).ConnectPhone();
        //Assert.That(_uut._oldId, Is.EqualTo(rfidEventArgs.ID));
        Assert.That(_uut._state, Is.EqualTo(Locked));
    }
    
    [Test] 
    public void TestOnRfidDetected_Available_PhoneNotConnected()
    {
        _uut._charger.Connected = false;
        _uut._state = Available;
        _uut.OnRfidDetected(this, new RfidEventArgs() {ID = 123456});
        _uut._display.Received(1).ConnectPhone();
        Assert.That(_uut._state, Is.EqualTo(Available));
    }
    
    
    
    
    // TEST ON DOOR OPEN
    [Test]
    public void TestOnDoorOpenedStateAvailible()
    {
        _uut._state = Available;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = true });
        _uut._logFile.Received(1).WriteLogEntry("Døren blev åbnet");
        _uut._display.Received(1).ConnectPhone();
        Assert.That(_uut._state, Is.EqualTo(DoorOpen));
    }
    
    [Test]
    public void TestOnDoorOpenedStateLocked()
    {
        _uut._state = Locked;
        _uut.OnDoorOpened(this, new DoorEventArgs() { DoorIsOpen = true });
        _uut._display.Received(1).DoorLocked();
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
    
    

}