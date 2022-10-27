namespace ChargingStation.lib.Interfaces;

public interface IDisplay
{
    //void OnDoorOpened(object source, DoorEventArgs eventArgs);
    //void OnDoorClosed(object source, DoorEventArgs eventArgs);
    
    void ConnectPhone();
    void ConnectionError();

    void LoadRFID();

    void Occupied();

    void RFIDError();

    void RemovePhone();

}