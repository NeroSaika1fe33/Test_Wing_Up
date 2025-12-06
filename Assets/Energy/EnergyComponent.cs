using UnityEngine;
using System;
using UnityEditor.SettingsManagement;

public class EnergyComponent : MonoBehaviour, IEnergy
{
    [Header("Settings")]
    [SerializeField] private float max = 100f;
    [SerializeField] private float regenPerSecond = 10f;

    private Energy energy;

    public float Current => energy.Current;
    public float Max => energy.Max;
    public bool IsFull => energy.IsFull;

    public float RegenPerSecond
    {
        get => energy.RegenPerSecond;
        set => energy.RegenPerSecond = value;
    }

    public event Action<float, float> OnEnergyChanged
    {
        add { energy.OnEnergyChanged += value; }
        remove { energy.OnEnergyChanged -= value; }
    }

    private void Awake()
    {
        energy = new Energy(max, 0f);
        energy.RegenPerSecond = regenPerSecond;
    }

    private void Update()
    {
        // ここで毎フレーム Tick。  
        // 条件付き回復にしたいなら、後で if(isAlive) などを挟めばよい。
        Tick(Time.deltaTime);
    }

    public void Tick(float deltaTime)
    {
        energy.Tick(deltaTime);
    }

    public bool TryConsume(float amount) => energy.TryConsume(amount);
    public void Restore(float amount) => energy.Restore(amount);

}
