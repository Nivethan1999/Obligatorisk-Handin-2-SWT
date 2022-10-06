using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.lib
{
    public interface IDoor
    {
        bool IsDoorOpen();

        void LockDoor();

        void UnlockDoor();




    }
}
