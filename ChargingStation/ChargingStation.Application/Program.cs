using ChargingStation;
using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;
using ChargingStation.test;

internal class Program
{
    private static void Main(string[] args)
    {
        Door door = new Door();
        RfidReader rfidReader = new RfidReader();
        Display display = new Display();
        ChargeControl chargeControl = new ChargeControl();
        LogFile log = new LogFile("ProgramLog.txt");

        var stationControl = new StationControl(rfidReader,  chargeControl, door, display, log);
        
        // Assemble your system here from all the classes

        var finish = false;
        do
        {
            string input;
            Console.WriteLine("Indtast E, O, C, R: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpened();
                    break;

                case 'C':
                    door.OnDoorClosed();
                    break;
                case 'R':
                    Console.WriteLine("Indtast RFID id: ");
                    var idString = Console.ReadLine();

                    var id = Convert.ToInt32(idString);
                    rfidReader.OnRfidDetected(id);
                    break;
            }
        } while (!finish);
    }
}