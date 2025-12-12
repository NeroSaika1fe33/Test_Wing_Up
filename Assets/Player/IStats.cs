using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    string ObjectName { get; set; }
    float maxSpeed { get; set; }
    float acceleration { get; set; }
    float weight { get; set; }
    bool isActive {  get; set; }

    List<IAbillity> abillity { get; set; }
}
