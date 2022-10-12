using ChargingStation.lib.Interfaces;

namespace ChargingStation.lib.Simulators;

public class LogFile : ILog
{
    public string fileName { set; get; }
    
    public LogFile(string fileName)
    {
        this.fileName = fileName;
    }
}