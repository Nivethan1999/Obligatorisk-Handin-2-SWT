namespace ChargingStation.lib;

public interface IDoor
{
    
    event EventHandler<DoorOpenEventArgs> DoorOpenEvent;

    event EventHandler<DoorClosedEventArgs> DoorCloseEvent;

    void OnDoorClose();

    void OnDoorOpen();

    void LockDoor();

    void UnlockDoor();

    

}

public class DoorOpenEventArgs : EventArgs
{
    public bool DoorOpen { get; set; }

}

public class DoorClosedEventArgs : EventArgs
{
    public bool DoorClose { get; set; }

}