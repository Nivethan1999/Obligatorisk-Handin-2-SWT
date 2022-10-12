namespace ChargingStation.lib.Interfaces;

public interface ILog
{
    string fileName { get; set; }

    void WriteLogEntry(string message, int id);
}