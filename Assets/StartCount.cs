using TMPro;
using UnityEngine;

public class StartCount : MonoBehaviour
{
    float c = 5;
    public TextMeshProUGUI CText;
    public int Get_Start_Count()
    {
        if (4 < c && c <= 5)
        {
            return 3;
        }
        else if (3 < c && c <= 4)
        {
            return 2;
        }
        else if (2 < c && c <= 3)
        {
            return 1;
        }
        else if (1 < c && c <= 2)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (-1 <= c)
        {
            c -= Time.deltaTime;
        }

        if (Get_Start_Count() >= 0) 
        { 
            CText.text = "" + Get_Start_Count(); 
        }
        else { CText.enabled = false; }
        
       
    }
}
