using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;


namespace ChargingStation.test
{
    public class TestDoor
    {
        private IDoor _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        
        // Door cant be open and locked at the same time
        [TestCase(true,false)]
        [TestCase(false,true)]
        [TestCase(false,false)]
        public void TestLockDoor(bool DoorOpen, bool DoorLocked)
        {
            _uut.DoorLocked = DoorLocked;
            _uut.DoorIsOpen = DoorOpen;
            _uut.LockDoor();

            Assert.That(_uut.DoorLocked, !_uut.DoorIsOpen ? Is.EqualTo(true) : Is.EqualTo(false));
        }
        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void TestUnlockDoor(bool DoorOpen, bool DoorLocked)
        {
            _uut.DoorLocked = DoorLocked;
            _uut.DoorIsOpen = DoorOpen;
            _uut.UnlockDoor();
            
            Assert.That(_uut.DoorLocked, Is.EqualTo(false));
 
        }

        // Testing Events
            [Test]
        public void TestDoorClosedEvent()
        {
            // Arrange
            var notified = false;
            _uut.OnDoorOpened();
            
            // Act
            _uut.DoorEvent += (sender, args) => { notified = true; };
            _uut.OnDoorClosed();
            
            // Assert
            Assert.That(notified, Is.True);
        }
        
        [Test]
        public void TestDoorOpenedEvent()
        {
            // Arrange
            var notified = false;
            _uut.OnDoorClosed();
            
            // Act
            _uut.DoorEvent += (sender, args) => { notified = true; };
            _uut.OnDoorOpened();
            
            // Assert
            Assert.That(notified, Is.True);
        }
        
        
        
    }
}