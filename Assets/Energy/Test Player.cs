using UnityEngine;
using UnityEngine.Rendering;

public class TestPlayer : MonoBehaviour
{
    [Header("Input")]
    public KeyCode boostKey = KeyCode.LeftShift;

    [SerializeField] private float currentSpeed = 0;
    [SerializeField] private float boostSpeed = 50;

    private IEnergy energy;

    private void Awake()
    {
        energy = GetComponent<IEnergy>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentSpeed < 99.0f)
            ++currentSpeed;

        if (currentSpeed > 100.0f)
            currentSpeed -= 0.1f;

        if (Input.GetKeyDown(boostKey))
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
            Debug.Log("ブーストできた、残るエネルギーは  " + energy.Current);
        }
        else
        {
            Debug.Log("エネルギー不足、ブーストできない！");
            return;
        }

        StartBoost();
    }

    private void StartBoost()
    {
        currentSpeed += boostSpeed;
    }
}
