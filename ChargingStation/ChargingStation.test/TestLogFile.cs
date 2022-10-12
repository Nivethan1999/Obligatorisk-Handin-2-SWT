using ChargingStation.lib.Interfaces;
using ChargingStation.lib.Simulators;

namespace ChargingStation.test;

public class Tests
{
    ILog _log;
    [SetUp]
    public void Setup()
    {
        _log = new LogFile("testFile.txt");
    }

    [Test]
    public void TestLogEntryWithId()
    {
        const string message = "test";
        const int id = 1;
        _log.WriteLogEntry(message, id);
        var last = File.ReadLines(_log.fileName).Last();
        var expected = $"{DateTime.Now}: {message}: {id}";
        Assert.That(last, Is.EqualTo(expected));
    }
    
    [Test]
    public void TestLogEntryWithoutId()
    {
        const string message = "test";
        _log.WriteLogEntry(message);
        var last = File.ReadLines(_log.fileName).Last();
        var expected = $"{DateTime.Now}: {message}";
        Assert.That(last, Is.EqualTo(expected));
    }
}