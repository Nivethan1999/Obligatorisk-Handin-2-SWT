namespace ChargingStation.lib;
public class DoorEventArgs : EventArgs
{
    public bool DoorIsOpen { get; set; }
}

public interface IDoor
{
    bool DoorLocked { get; set; }
    bool DoorIsOpen { get; set; }
    
    int _currentId { get; set; }


    public event EventHandler<DoorEventArgs> DoorEvent; 

    void LockDoor();

    void UnlockDoor();

    virtual void OnDoorOpened() { }

    virtual void OnDoorClosed () { }
}
