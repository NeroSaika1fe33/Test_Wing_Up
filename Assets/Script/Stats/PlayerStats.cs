using System;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class PlayerStats : MonoBehaviour, IStats
{
    public float maxSpeed { get; set; }
    public float acceleration { get; set; }
    public float weight { get; set; }
    public string abilityName { get; set; } = null;

    [SerializeField] private PartsDataManager PDManager;

    private SetParts Car;

    public Part[] parts { get; private set; } = new Part[3];
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
    void Start()
    {
        InitParts();
        UpdatePartsStats();
        
    }

    protected void InitParts()
    {
        if (parts != null)
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
        else
        {
            Debug.LogError("CarPartsがアサインせれていない！！");
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
        if (LevelManager.Instance.GetCurrentScene() == SceneList.Car_Selection)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                UpdateParts();
                UpdatePartsStats();
                Debug.Log("Stats: " + maxSpeed + "," + acceleration + "," + weight);
            }
        }

        if (LevelManager.Instance.GetCurrentScene() == SceneList.Result)
        {
            //Debug.Log("Stats: " + maxSpeed + "," + acceleration + "," + weight);
            PlayerDataManager.Instance.SetPlayer(this);
        }
    }
}
