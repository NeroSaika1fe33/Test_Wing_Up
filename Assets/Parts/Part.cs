using UnityEngine;

public class Part : MonoBehaviour, IPart
{
    public string partsName { get; set; }
    public float maxSpeed { get; set; }
    public float acceleration { get; set; }
    public float weight { get; set; }
    public string partsType { get; set; }

    public IAbillity abillity { get; set; }

    public PartsDataManager PDManager;

    public void Start()
    {
        partsName = gameObject.name;
    }

    public string GetPartsName()
    {
        return partsName;
    }

    public float PlusMaxSpeed(float _maxSpeed)
    {
        return _maxSpeed + maxSpeed;
    }
    public float PlusAcceleration(float _acceleration)
    {
        return _acceleration + acceleration;
    }
    public float PlusWeight(float _weight)
    {
        return _weight + weight;
    }


}
