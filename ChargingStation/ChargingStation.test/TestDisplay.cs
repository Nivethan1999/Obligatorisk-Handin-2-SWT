using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;


namespace ChargingStation.test
{
    public class TestDisplay
    {
        IDisplay display_;

        [SetUp]

        public void Setup()
        {
            display_ = new Display();
        }

        [Test]
        public void ConnectPhoneTest()
        {
            Assert.That(display_.ConnectPhone, Is.True);
            
        }
    }
}
