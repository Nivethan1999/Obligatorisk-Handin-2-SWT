using ChargingStation.lib.Interfaces;

namespace ChargingStation.lib.Simulators;

public class Display : IDisplay
{
    public void ConnectPhone()
    {
        Console.WriteLine("Tilslut telefon");        
    }

    public void ConnectionError()
    {
        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Pr�v igen.");
    }

    public void LoadRFID()
    {
        Console.WriteLine("Skabet er l�st og din telefon lades. Brug dit RFID tag til at l�se op.");

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
        Console.WriteLine("Tag din telefon ud af skabet og luk d�ren");

    }
}