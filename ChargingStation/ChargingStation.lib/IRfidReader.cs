using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.lib
{
     public interface IRfidReader
     {
          public void RfidDetected(int Id);

          public void IsConnected();

          public void CheckId(int oldid, int id);

     }
}
