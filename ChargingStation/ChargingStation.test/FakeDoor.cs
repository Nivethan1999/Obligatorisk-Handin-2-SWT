﻿using ChargingStation.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.test
{
     public class FakeDoor :IDoor
     {
          public bool DoorLocked { get; set; }
          public bool DoorIsOpen { get; set; }
          public int _currentId { get; set; }

          private DoorEventArgs _doorEventArgs = new DoorEventArgs()
          {
               DoorIsOpen = false
          };

          // Events
          public event EventHandler<DoorEventArgs>? DoorEvent;

          // Constructor
          public FakeDoor()
          {
               DoorLocked = false;
               DoorIsOpen = false;
          }

          // Methods
          public void LockDoor()
          {
               if (DoorLocked || DoorIsOpen) return;
               DoorLocked = true;
          }

          public void UnlockDoor()
          {
               if (!DoorLocked) return;
               DoorLocked = false;
          }

          public virtual void OnDoorOpened()
          {
               if (DoorLocked || DoorIsOpen) return;

               DoorIsOpen = true;
               _doorEventArgs.DoorIsOpen = true;
               DoorEvent?.Invoke(this, _doorEventArgs);
          }

          public virtual void OnDoorClosed()
          {
               if (DoorLocked || !DoorIsOpen) return;

               DoorIsOpen = false;
               _doorEventArgs.DoorIsOpen = false;
               DoorEvent?.Invoke(this, _doorEventArgs);
          }
     }
}
