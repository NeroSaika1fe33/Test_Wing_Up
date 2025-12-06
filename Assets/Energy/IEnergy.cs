using System;
using UnityEngine;


public interface IEnergy
{
    float Current { get; }
    float Max { get; }
    bool IsFull { get; }

    /// <summary>ŠÔŒo‰ß‚É‚æ‚é‰ñ•œˆ—</summary>
    void Tick(float deltaTime);
    /// <summary>w’è—Ê‚ğÁ”ïB‘«‚è‚È‚¯‚ê‚Îfalse‚ğ•Ô‚·</summary>
    bool TryConsume(float amount);
    /// <summary>ˆê‰ñŒø‰Ê‚Ì‰ñ•œ</summary>
    void Restore(float amount);

    event System.Action<float, float> OnEnergyChanged;//(current,max)
}
