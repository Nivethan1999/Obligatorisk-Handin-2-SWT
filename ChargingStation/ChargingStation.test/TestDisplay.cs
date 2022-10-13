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
        Display display_;
        private StringWriter output;

        [SetUp]

        public void Setup()
        {
            output = new StringWriter();
            display_ = new Display();
            Console.SetOut(output);
        }

        [Test]
        public void ConnectPhoneTest()
        {
            display_.ConnectPhone();
            Assert.That(output.ToString(), Contains.Substring("Tilslut telefon"));
        }

        [Test]

        public void OccupiedTest()
        {
            display_.Occupied();
            Assert.That(output.ToString(), Contains.Substring("Ladeskab optaget"));
        }

        [Test]

        public void RFIDErrorTest()
        {
            display_.RFIDError();
            Assert.That(output.ToString(), Contains.Substring("Forkert RFID tag"));
        }

        [Test]

        public void RemovePhoneTest()
        {
            display_.RemovePhone();
            Assert.That(output.ToString(), Contains.Substring("Tag din telefon ud af skabet og luk døren"));
        }

    }

        public void ConnectionError()
        {
             display_.ConnectionError();
             Assert.That(output.ToString(), Contains.Substring("Din telefon er ikke ordentlig tilsluttet. Prøv igen."));
        }
        [Test]
        public void LoadRFID()
        {
             display_.LoadRFID();
             Assert.That(output.ToString(), Contains.Substring("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."));
        }

     }
}
