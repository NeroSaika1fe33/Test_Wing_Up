using UnityEngine;

public interface IPart
{
    string partsName { get; set; }
    float maxSpeed { get; set; }
    float acceleration { get; set; }
    float weight { get; set; }
    string partsType {  get; set; }

    IAbillity abillity { get; set; }

}
