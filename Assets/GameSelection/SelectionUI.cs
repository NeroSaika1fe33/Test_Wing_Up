using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionUI : MonoBehaviour
{
    public TMP_Text categoryText;
    public TMP_Text itemText;

    //public PartsDataManager partsDataManager;
    //Carのスクリプトを取得
    public SetParts Car;

    //Changes
    string[] categories = { "Body", "Tire", "Wing" };
    string[][] items = {
        new string[] { "Normal_Body A", "Normal_Body B" },
        new string[] { "Normal_Tire A", "Normal_Tire B" },
        new string[] { "Normal_Wing A", "Normal_Wing B" }
    };
    //player now select category(left/right)
    int currentCategory = 0;
    //select parts save
    int[] selected = { 0, 0, 0 };

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // left/right
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentCategory = (currentCategory - 1 + categories.Length) % categories.Length;

            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentCategory = (currentCategory + 1) % categories.Length;
            UpdateUI();
        }

        // up/down
        if (Input.GetKeyDown(KeyCode.W))
        {
            selected[currentCategory]--;
            if (selected[currentCategory] < 0)
                selected[currentCategory] = items[currentCategory].Length - 1;
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            selected[currentCategory]++;
            if (selected[currentCategory] >= items[currentCategory].Length)
                selected[currentCategory] = 0;
            UpdateUI();
        }

        // Enter to ingame
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelection();
        }
    }

    void UpdateUI()
    {
        categoryText.text = "Category: " + categories[currentCategory];
        itemText.text = "Item: " + items[currentCategory][selected[currentCategory]];

        //パーツ名を更新
        if(currentCategory==0)
        Car.UpdateBodyParts(items[currentCategory][selected[currentCategory]]);
        else if(currentCategory==1)
        Car.UpdateTireParts(items[currentCategory][selected[currentCategory]]);
        else if(currentCategory==2)
        Car.UpdateWingParts(items[currentCategory][selected[currentCategory]]);
    }

    void ConfirmSelection()
    {
        GameSelectionData.body = selected[0];
        GameSelectionData.wheel = selected[1];
        GameSelectionData.wing = selected[2];

        SceneManager.LoadScene("Ingame_test");
    }
}
