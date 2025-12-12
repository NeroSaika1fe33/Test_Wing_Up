using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements.Experimental;

public class PartsStats
{
    private int partsNum { get; set; } = 3;
    public List<Transform> partsPos;
    public List<Part> Parts;
    public PartsDataManager PDManager;

    public void InitParts(string _partsName)
    {
        for(int i = 0; i < partsNum; ++i)
        {
            Parts[i].partsName = _partsName;
            //PDManager.Get_PartsData_int(_partsName, "ハンドリング");
            Parts[i].maxSpeed = PDManager.Get_PartsData_int(_partsName, "最高速度");
            Parts[i].acceleration = PDManager.Get_PartsData_int(_partsName, "加速度");
            Parts[i].weight = PDManager.Get_PartsData_int(_partsName, "重量");
            Parts[i].partsType = PDManager.Get_PartsType(_partsName);
        }

    }
}
