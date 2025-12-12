using UnityEngine;

public interface IAbillity
{
    string Name { get; set; }
    bool canUse();
    void useAbility();
}
