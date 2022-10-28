using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.lib;
using ChargingStation.lib.Simulators;
using NSubstitute;
using UsbSimulator;

namespace ChargingStation.test
{
    public class TestStationControl
    {
        private StationControl _uut;
        private LogFile _logfile;
        private Display _display;
        private Door _door;
        private RfidReader _rfidReader;
        private ChargeControl _chargeControl;

        [SetUp]
        public void Setup()
        {
            _logfile = Substitute.For<LogFile>("text.txt");
            _display = Substitute.For<Display>();
            _rfidReader = Substitute.For<RfidReader>();
            _chargeControl = Substitute.For<ChargeControl>(_display,Substitute.For<UsbChargerSimulator>());
            _door = Substitute.For<Door>();
            _uut = new StationControl(_rfidReader, _chargeControl,_door,_display,_logfile);
        }

        

        [Test]
        public void OnDoorOpened()
        {
            _uut._state = StationControl.ChargingStationState.DoorOpen;
            Assert.That(_uut._state,Is.EqualTo(StationControl.ChargingStationState.DoorOpen));
        }

        [Test]
        public void OnDoorOpenedLock()
        {
            _uut._state = StationControl.ChargingStationState.Locked;
            _display.Received(1).DoorLocked();
            Assert.That(_uut._state,Is.EqualTo(StationControl.ChargingStationState.Locked));

        }
    }
}
