using UnityEngine;
using UnityEngine.UI;  
using TMPro;
//Car‚Ì‘¬“xUI
public class SpeedoUI : MonoBehaviour
{
    public TMP_Text speedText;
    public Rigidbody carRb; 

    void Update()
    {
        //m/s’PˆÊ‚©‚çkm/s’PˆÊ“]Š·
        float currentSpeed = carRb.linearVelocity.magnitude * 3.6f;  

        speedText.text = "Speed: " + Mathf.Round(currentSpeed).ToString() + " km/h";
    }
}
