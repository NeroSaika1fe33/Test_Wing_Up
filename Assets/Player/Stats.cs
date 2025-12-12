using System.Collections.Generic;
using UnityEngine;

public class Stats:IStats
{
    public string ObjectName { get; set; }
    public float maxSpeed { get; set; }
    public float acceleration { get; set; }
    public float weight { get; set; }
    public bool isActive { get; set; }

    public List<IAbillity> abillity { get; set; }


}
