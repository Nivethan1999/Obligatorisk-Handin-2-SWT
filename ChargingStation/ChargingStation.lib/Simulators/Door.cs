namespace ChargingStation.lib;

public class Door : IDoor
{
    // Constants
    public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
    public event EventHandler<DoorClosedEventArgs> DoorCloseEvent;

    public bool DoorState { get; private set; }

    public bool DoorLocked { get; private set; }

    public Door()
    {
        DoorState = false;
        DoorLocked = false;
    }

    

    public void LockDoor()
    {
        throw new NotImplementedException();
    }

    public void UnlockDoor()
    {
        throw new NotImplementedException();
    }

    public void OnDoorOpen()
    {
        throw new NotImplementedException();
    }

    public void OnDoorClose()
    {
        throw new NotImplementedException();
    }

    //public void OnDoorOpen()
    //{
    //    DoorOpenEvent?.Invoke(this, DoorState);
    //}
    
    //public void OnDoorClose()
    //{
    //    DoorCloseEvent?.Invoke(this, Close);
    //}
}