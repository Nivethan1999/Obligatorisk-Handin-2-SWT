using ChargingStation.lib.Interfaces;

namespace ChargingStation.lib.Simulators;

public class LogFile : ILog
{
    public string fileName { set; get; }
    public void WriteLogEntry(string message, int id)
    {
        using var writer = File.AppendText(fileName);
        writer.WriteLine(DateTime.Now + ": {0}: {1}", message, id);
    }

    public LogFile(string fileName)
    {
        this.fileName = fileName;
        
    }
}