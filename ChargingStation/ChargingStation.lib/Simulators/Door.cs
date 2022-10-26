namespace ChargingStation.lib;

public class Door : IDoor
{
    // Constants
    public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
    public event EventHandler<DoorClosedEventArgs> DoorCloseEvent;

    private DoorOpenEventArgs Open = new DoorOpenEventArgs
    {
        DoorOpen = false
    };

    private DoorClosedEventArgs Close = new DoorClosedEventArgs
    {
        DoorClose = false
    };
    public bool DoorLocked { get; private set; }

    public Door()
    {
        
        DoorLocked = false;
    }

    public void LockDoor()
    {
        DoorLocked = true;
    }

    public void UnlockDoor()
    {
        DoorLocked = false;
    }

    public void OnDoorOpen()
    {
        DoorOpenEvent?.Invoke(this, Open);
    }
    
    public void OnDoorClose()
    {
        DoorCloseEvent?.Invoke(this, Close);
    }
}