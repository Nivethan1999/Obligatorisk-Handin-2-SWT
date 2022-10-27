using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;
using ChargingStation.test;

namespace ChargingStation
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum ChargingStationState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private ChargingStationState _state;
        private IChargeControl _charger = new ChargeControl();
        private int _oldId;
        private IDoor _door = new Door();
        private IDisplay _display = new Display();
        private ILog _logFile = new LogFile("logfile.txt");
        private IRfidReader _reader = new RfidReader();
        

        
        // Her mangler constructor
        public StationControl(IRfidReader reader, IChargeControl charger, IDoor door, IDisplay display, ILog logFile)
        {
            _state = ChargingStationState.Available;
            _reader = reader;
            _charger = charger;
            _door = door;
            _display = display;
            _logFile = logFile;
            
            //_door.DoorEvent += _display.OnDoorOpened;
            //_door.DoorEvent += _display.OnDoorClosed;

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case ChargingStationState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        
                        _door.LockDoor();
                        _charger.StartCharge(); 
                        _oldId = id;
                        _logFile.WriteLogEntry("Skab låst med RFID", id);
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
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logFile.WriteLogEntry("Skab låst op med RFID", id);

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
        private void DoorClosed()
        {
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
                    _display.LoadRFID();
                    _state = ChargingStationState.Available;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Triggers til DoorOpened
        private void DoorOpened()
        {
             switch (_state)
             {
                  case ChargingStationState.Available:
                       _logFile.WriteLogEntry("Døren blev åbnet");
                       _display.ConnectPhone();
                       _state = ChargingStationState.DoorOpen;
                       break;

                  case ChargingStationState.DoorOpen:
                       // Ignore
                       break;

                  case ChargingStationState.Locked:
                       Console.WriteLine("Døren er låst");
                       break;
                  default:
                       throw new ArgumentOutOfRangeException();
             }
        }
        // Her mangler de andre trigger handlere
    }
}