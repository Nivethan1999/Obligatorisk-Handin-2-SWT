namespace ChargingStation.lib.Interfaces;

public interface IDisplay
{
    
    void ConnectPhone();
    void ConnectionError();

    void LoadRFID();

    void Occupied();

    void RFIDError();
    
    void PhoneConnected();

    void RemovePhone();
    
    void ChargeError();

}