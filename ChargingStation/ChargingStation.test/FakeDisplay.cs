﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.lib.Interfaces;

namespace ChargingStation.test
{
     public class FakeDisplay : IDisplay
     {
          public void ConnectPhone()
          {
               Console.WriteLine("Tilslut telefon");
          }

          public void ConnectionError()
          {
               Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
          }

          public void LoadRFID()
          {
               Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");

          }

          public void Occupied()
          {
               Console.WriteLine("Ladeskab optaget");
          }

          public void RFIDError()
          {
               Console.WriteLine("Forkert RFID tag");
          }

          public void RemovePhone()
          {
               Console.WriteLine("Tag din telefon ud af skabet og luk døren");

          }
     }
}
