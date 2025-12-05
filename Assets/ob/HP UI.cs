using UnityEngine;
using UnityEngine.UI;

public class CarHPUI : MonoBehaviour
{
    public Image[] hp; // 3 heart image
   

    public void UpdateHP(int currentHP)
    {
        for (int i = 0; i < hp.Length; i++)
        {

            hp[i].enabled = (i < currentHP);
        }
      
    }
}
