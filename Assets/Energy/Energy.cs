using System.ComponentModel;
using UnityEngine;

public class Energy : IEnergy
{
    public float Current { get; private set; }
    public float Max { get; private set; }
    public bool IsFull => Current >= Max - 0.01;
    public float RegenPerSecond { get; set; } = 10f;

    public event System.Action<float, float> OnEnergyChanged;

    public Energy(float _Max, float _Initial = 0f)
    {
        Max = _Max;
        Current = Mathf.Clamp(_Initial, 0f, _Max);
        RaiseChanged();
    }

    public void Tick(float deltaTime)
    {
        if (IsFull) return;

        Current += RegenPerSecond * deltaTime;
        if (Current > Max) Current = Max;
        RaiseChanged();
    }

    public bool TryConsume(float amount)
    {
        if (Current < amount) return false;

        Current -= amount;

        if (Current < 0f) Current = 0f;

        RaiseChanged();
        return true;
    }

    public void Restore(float amount)
    {
        if(amount<=0) return;

        Current += amount;

        if(Current > Max) Current = Max;
        RaiseChanged() ;
    }

    //OnEnergyChangedƒCƒxƒ“ƒg
    private void RaiseChanged()
    {
        OnEnergyChanged?.Invoke(Current, Max);
    }
}
