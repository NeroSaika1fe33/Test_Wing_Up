using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnergyGaugeUI : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Slider EnergyGaugeSlider;

    [Header("Energy Source")]
    [SerializeField] private MonoBehaviour energySourceBehaviour;
    private IEnergy Energy;

    private void Awake()
    {
        if (EnergyGaugeSlider == null)
        {
            Debug.LogError("EnergeGaugeUI　が設定されていない！");
        }

        if (energySourceBehaviour is IEnergy e)
        {
            Energy = e;
        }
        else
        {
            Energy = GetComponentInParent<IEnergy>();
        }

        if (Energy == null)
        {
            Debug.LogError("[EnergyGaugeUI]のソースIEnergy見つからない！");
            return;
        }

        UpdateGauge(Energy.Current, Energy.Max);

        Energy.OnEnergyChanged += OnEnergyChanged;
    }

    private void OnDestroy()
    {
        if (Energy != null)
        {
            Energy.OnEnergyChanged -= OnEnergyChanged;
        }
    }

    private void OnEnergyChanged(float current, float max)
    {
        UpdateGauge(current, max);
    }

    private void UpdateGauge(float current, float max)
    {
        if (EnergyGaugeSlider == null) return;
        if (max <= 0f) max = 1f;

        float normalized = Mathf.Clamp01(current / max);
        EnergyGaugeSlider.value = normalized;
    }
}
