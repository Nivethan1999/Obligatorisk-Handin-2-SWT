namespace ChargingStation.lib.Interfaces;

public interface IDisplay
{
    void ConnectPhone();
    void ConnectionError();

    void LoadRFID();

    void Occupied();

    void RFIDError();

    void RemovePhone();

}