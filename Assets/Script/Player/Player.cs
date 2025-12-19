using UnityEngine;

public class PlayerSaveData
{
    public string name;
    public int id;
    public string[] PartsName = new string[3];
    public float maxSpeed;
    public float acceleration;
    public float weight;
    public string abilityName;
    public string result;
    public PlayerSaveData(string _name, int _id, string[] _partsName, float _maxSpeed, float _acceleration, float _weight, string result, string abilityName)
    {
        this.name = _name;
        this.id = _id;
        PartsName = _partsName;
        this.maxSpeed = _maxSpeed;
        this.acceleration = _acceleration;
        this.weight = _weight;
        this.result = result;
        this.abilityName = abilityName;
    }
}
