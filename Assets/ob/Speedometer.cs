using UnityEngine;
using UnityEngine.UI;  
using TMPro;
public class Speedometer : MonoBehaviour
{
    public TMP_Text speedText;  // UI Text
    public Rigidbody carRb; // Rigidbody

    void Update()
    {
       
        float currentSpeed = carRb.linearVelocity.magnitude * 3.6f;  //m/s->km/s

        
        speedText.text = "Speed: " + Mathf.Round(currentSpeed).ToString() + " km/h";
    }
}
