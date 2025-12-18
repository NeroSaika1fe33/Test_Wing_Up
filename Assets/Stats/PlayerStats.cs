using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerStats : MonoBehaviour, IStats
{
    //public PartsStats partsStats { get; private set; }
    public float maxSpeed { get; set; } = 100.0f;
    public float acceleration { get; set; } = 50.0f;
    public float weight { get; set; } = 10.0f;
    public string abilityName { get; set; } = null;

    public PartsDataManager PDManager;

    private SetParts Car;

    private Part[] parts = new Part[3];
    private void Awake()
    {
        Car = GetComponentInParent<SetParts>();
        //配列初期化
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = new Part();
            parts[i].ability = null;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitParts();
        //一回更新
        UpdatePartsStats();
    }

    protected void InitParts()
    {
        parts[0].partsName = Car.BodyPrefabName;
        parts[1].partsName = Car.MainspringPrefabName;
        parts[2].partsName = Car.TirePrefabName;
        for (int i = 0; i < parts.Length; i++)
        {
            string Name = PDManager.Get_PartsName(parts[i].partsName);
            parts[i].maxSpeed = PDManager.Get_PartsData_int(Name, "最高速度");
            parts[i].acceleration = PDManager.Get_PartsData_int(Name, "加速度");
            parts[i].weight = PDManager.Get_PartsData_int(Name, "重量");
            //parts[i].ability.Name = PDManager.Get_PartsData_string(parts[i].partsName, "アビリティ");
        }

    }
    //パーツステータス更新
    protected void UpdatePartsStats()
    {
        maxSpeed = parts[0].maxSpeed + parts[1].maxSpeed + parts[2].maxSpeed;
        acceleration = parts[0].acceleration + parts[1].acceleration + parts[2].acceleration;
        weight = parts[0].weight + parts[1].weight + parts[2].weight;
    }
    //パーツ内容の更新
    protected void UpdateParts()
    {
        parts[0].partsName = Car.BodyPrefabName;
        parts[1].partsName = Car.MainspringPrefabName;
        parts[2].partsName = Car.TirePrefabName;
        for (int i = 0; i < parts.Length; i++)
        {
            string Name = PDManager.Get_PartsName(parts[i].partsName);
            parts[i].maxSpeed = PDManager.Get_PartsData_int(Name, "最高速度");
            parts[i].acceleration = PDManager.Get_PartsData_int(Name, "加速度");
            parts[i].weight = PDManager.Get_PartsData_int(Name, "重量");
            //parts[i].ability.Name = PDManager.Get_PartsData_string(parts[i].partsName, "アビリティ");
            //Debug.Log(parts[i].partsName + " " + parts[i].maxSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            UpdateParts();
            UpdatePartsStats();
            Debug.Log(maxSpeed);
        }
    }
}
