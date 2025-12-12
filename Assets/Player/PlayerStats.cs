using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerStats : MonoBehaviour,IStats
{
    public PartsStats partsStats {  get; private set; }
    public string ObjectName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float maxSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool isActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public List<IAbillity> abillity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitStats();
        if(partsStats != null)
        {
        InitPartsStats();
        Debug.Log(maxSpeed + " " + acceleration + " " + weight);

        }
        else
        {
            Debug.Log("PartsStats‚ÍNULL!!");
        }

    }

    protected void InitPartsStats()
    {
        partsStats.PDManager = gameObject.GetComponentInChildren<PartsDataManager>();
        for(int i = 0; i < partsStats.Parts.Count; ++i)
        {
            partsStats.Parts[i] = partsStats.partsPos[i].GetChild(0).GetComponent<Part>();
        }

        for(int i = 0; i < partsStats.Parts.Count; ++i)
        {
            maxSpeed = partsStats.Parts[i].PlusMaxSpeed(maxSpeed);
            acceleration = partsStats.Parts[i].PlusAcceleration(acceleration);
            weight = partsStats.Parts[i].PlusWeight(weight);
        }

    }

    protected void UpdateStats()
    {

    }
    protected void InitStats()
    {
        ObjectName = null;
        maxSpeed = 100.0f;
        acceleration = 5.0f;
        weight = 2;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
