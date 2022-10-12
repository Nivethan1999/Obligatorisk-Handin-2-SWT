using ChargingStation.lib;
using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;

internal class Program
{
    private static void Main(string[] args)
    {
        IDoor door = new Door();
        IRfidReader rfidReader = new RfidReader();
        
        
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
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose();
                    break;

                case 'R':
                    Console.WriteLine("Indtast RFID id: ");
                    var idString = Console.ReadLine();

                    var id = Convert.ToInt32(idString);
                    rfidReader.OnRfidRead(id);
                    break;
            }
        } while (!finish);
    }
}