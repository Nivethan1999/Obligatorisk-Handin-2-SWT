namespace ChargingStation.lib;

public interface IDoor
{
    public void LockDoor();
    public void UnlockDoor();
    
    public void OnDoorOpen();
    public void OnDoorClose();
    
}