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
    public class TestRfid
    {
        private IDoor _door;
        private IRfidReader _uut;
        private IChargeControl _chargeControl;
        private IDisplay _display;
        private ILog _logFile;
        private StationControl _stationControl;

        private int detectedId;
        
        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();
            _uut.RfidEvent += OnRfidDetected;
        }

        private void OnRfidDetected(object source, RfidEventArgs eventArgs)
        {
            detectedId = eventArgs.ID;
        }
       
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestRfidReader(int id)
        {
            _uut.OnRfidDetected(id);
            Assert.That(detectedId, Is.EqualTo(id));
        }
        
        

        
    }
}