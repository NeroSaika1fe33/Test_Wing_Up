using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    private car_manager car_Manager;
	public TextMeshProUGUI TimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject resultObj = GameObject.Find("Car");

        Debug.Log(resultObj);
        if (resultObj != null)
        {
            car_Manager = resultObj.GetComponent<car_manager>();
            TimeText.text = $"{car_Manager.Get_GoalTime_m():D2}:{car_Manager.Get_GoalTime_s():D2}:{car_Manager.Get_GoalTime_ms():D3}";
        }
        else
        {
            TimeText.text = $"__:__:___";
        }
	}

    // Update is called once per frame
    void Update()
    {
       
    }
}
