using ChargingStation.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.test
{
     public class FakeRfidReader :IRfidReader
     {

          public event EventHandler<RfidEventArgs>? RfidEvent;

          public void OnRfidDetected(int id)
          {
               RfidEvent?.Invoke(this, new RfidEventArgs { ID = id });
          }
     }
}
