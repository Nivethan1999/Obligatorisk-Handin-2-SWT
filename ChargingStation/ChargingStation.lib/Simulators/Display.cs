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
        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
    }

    public void LoadRFID()
    {
        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
    }
    
    public void LockWithRfid()
    {
        Console.WriteLine("Brug dit RFID tag til at låse skabet..");
    }
    public void PhoneConnected()
    {
        Console.WriteLine("Telefonen er tilsluttet korrekt");
    }

    public void ChargeError()
    {
        Console.WriteLine("Fejl i opladning");
    }

    public void DoorLocked()
    {
        Console.WriteLine("Døren er låst");
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