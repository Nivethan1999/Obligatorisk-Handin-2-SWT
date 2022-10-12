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

    public bool DoorState { get; private set; }

    public bool DoorLocked { get; private set; }

    public Door()
    {
        DoorState = false;
        DoorLocked = false;
    }

    public void IsDoorClosed()
    {
        if (!DoorState || DoorLocked)
        {
            Close.
        }
    }

    public void IsDoorOpen()
    {
        
    }

    public void LockDoor()
    {
        throw new NotImplementedException();
    }

    public void UnlockDoor()
    {
        throw new NotImplementedException();
    }
}