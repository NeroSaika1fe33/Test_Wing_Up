using UnityEngine;

public class Player
{
    public string name;
    public int id;
    public string[] PartsName = new string[3];
    public float maxSpeed;
    public float acceleration;
    public float weight;

    public Player(string _name, int _id, string[] _partsName, float _maxSpeed, float _acceleration, float _weight)
    {
        this.name = _name;
        this.id = _id;
        PartsName = _partsName;
        this.maxSpeed = _maxSpeed;
        this.acceleration = _acceleration;
        this.weight = _weight;
    }
}
