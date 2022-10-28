using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;
using ChargingStation.test;
using UsbSimulator;

namespace ChargingStation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum ChargingStationState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public ChargingStationState _state;
        private IUsbCharger _usbcharger;
        private IDisplay _display;
        private IChargeControl _charger;
        private int _oldId;
        public IDoor _door;
        private ILog _logFile;
        private IRfidReader _reader;
        

        
        // Her mangler constructor
        public StationControl(IRfidReader reader, IChargeControl charger, IDoor door, IDisplay display, ILog logFile)
        {
            _state = ChargingStationState.Available;
            _reader = reader;
            _charger = charger;
            _door = door;
            _display = display;
            _logFile = logFile;

            door.DoorEvent += OnDoorOpened;
            door.DoorEvent += OnDoorClosed;
            
            reader.RfidEvent += OnRfidDetected;

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void OnRfidDetected(object source, RfidEventArgs eventArgs)
        {
            switch (_state)
            {
                case ChargingStationState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        
                        _door.LockDoor();
                        _charger.StartCharge(); 
                        _oldId = eventArgs.ID;
                        _logFile.WriteLogEntry("Skab låst med RFID", eventArgs.ID);
                        _display.Occupied();
                        _state = ChargingStationState.Locked;
                    }
                    else
                    {
                        _display.ConnectPhone();
                    }

                    break;

                case ChargingStationState.DoorOpen:
                    // Ignore
                    break;

                case ChargingStationState.Locked:
                    // Check for correct ID
                    if (eventArgs.ID == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logFile.WriteLogEntry("Skab låst op med RFID", eventArgs.ID);

                        _display.RemovePhone();
                        _state = ChargingStationState.Available;
                    }
                    else
                    {
                        _display.RFIDError();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Triggers til DoorClosed
        public void OnDoorClosed(object source, DoorEventArgs eventArgs)
        {
            if (eventArgs.DoorIsOpen) return;
            switch (_state)
            {
                case ChargingStationState.Available:
                    // ignore
                    break;
                case ChargingStationState.Locked:
                    // ignore
                    break;
                case ChargingStationState.DoorOpen:
                    _logFile.WriteLogEntry("Døren blev lukket");
                    _display.LockWithRfid();
                    _state = ChargingStationState.Available;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine("CURRENT STATE: " + _state);
        }

        // Triggers til DoorOpened
        public void OnDoorOpened(object source, DoorEventArgs eventArgs)
        {
            if (!eventArgs.DoorIsOpen) return;
             switch (_state)
             {
                  case ChargingStationState.Available:
                       _logFile.WriteLogEntry("Døren blev åbnet");
                       _display.ConnectPhone();
                       _state = ChargingStationState.DoorOpen;
                       Console.WriteLine("CURRENT STATE: " + _state);
                       break;

                  case ChargingStationState.DoorOpen:
                       // Ignore
                       break;

                  case ChargingStationState.Locked:
                       //Console.WriteLine("Døren er låst");
                       _display.DoorLocked();
                       break;
                  default:
                       throw new ArgumentOutOfRangeException();
             }
             
        }

        // Her mangler de andre trigger handlere
    }
}