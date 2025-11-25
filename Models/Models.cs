using System.Dynamic;
using System.Runtime.InteropServices;

namespace Models;

public class DroneModel
{
    public string Name {get; set;}
    public int MaxCheckPoints {get; set;}
    public int DelayMs {get; set;}

    public DroneModel (string name, int maxCheckPoints, int delayMs)
    {
        Name = name;
        MaxCheckPoints = maxCheckPoints;
        DelayMs = delayMs;
    }
}
