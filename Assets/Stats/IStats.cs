using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    float maxSpeed { get; set; }
    float acceleration { get; set; }
    float weight { get; set; }

    string abilityName { get; set; }
}
