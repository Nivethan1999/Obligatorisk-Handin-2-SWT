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
          public event EventHandler<RFIDDetectedEvent>? DetectedEvent;
          public void OnRfidRead(int id)
          {
               throw new NotImplementedException();
          }

          public void ReadRfid(int id)
          {
               DetectedEvent?.Invoke(this, new RFIDDetectedEvent { ID = id });
          }
     }
}
