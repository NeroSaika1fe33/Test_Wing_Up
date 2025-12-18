using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements.Experimental;

public class PartsStats : IStats
{
    public float maxSpeed { get; set; }
    public float acceleration { get; set; }
    public float weight { get; set; }
    public string abilityName { get; set; }
    private int partsNum { get; set; } = 3;

    //public List<Transform> partsPos;
    public Part[] Parts = new Part[3];

    public PartsStats(Part[] parts)
    {
        Parts = parts;
    }

    public void InitStats()
    {
        for (int i = 0; i < partsNum; ++i)
        {
            maxSpeed += Parts[i].maxSpeed;
            acceleration += Parts[i].acceleration;
            weight += Parts[i].weight;
        }
    }
}
