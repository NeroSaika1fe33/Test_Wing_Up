using UnityEngine;
using UnityEngine.Rendering;

public class TestPlayer : MonoBehaviour
{
    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    //[SerializeField] private EnergyComponent BoostGauge;

    [SerializeField] private float CurrentSpeed = 0;
    [SerializeField] private float BoostSpeed = 50;

    private IEnergy energy;

    private void Awake()
    {
        energy = GetComponent<IEnergy>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (CurrentSpeed < 99.0f)
            ++CurrentSpeed;

        if (CurrentSpeed > 100.0f)
            CurrentSpeed -= 0.1f;

        if (Input.GetKeyDown(dashKey))
        {
            TryBoost();
        }

    }

    private void TryBoost()
    {
        if (energy == null) return;

        if (energy.IsFull)
        {
            energy.TryConsume(99.0f);
        }
        else
        {
            Debug.Log("エナジー不足、ブーストできない！");
            return;
        }

        StartBoost();
    }

    private void StartBoost()
    {
        CurrentSpeed += BoostSpeed;
        Debug.Log(energy.Current);
    }
}
