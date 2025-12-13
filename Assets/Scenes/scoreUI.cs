using TMPro;
using UnityEngine;

public class scoreUI : MonoBehaviour
{
    public car_situation car_Situation;
    public TextMeshProUGUI TimeText;
    public goal_contact goal_Contact;
    //public car_manager player;   // ← Playerのスクリプトを参照

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (car_Situation.Get_Steat() != car_situation.Steat.Goal)
        TimeText.text = $"{goal_Contact.Get_Time_m():D2}:{goal_Contact.Get_Time_s():D2}:{goal_Contact.Get_Time_ms():D3}";
    }
}
