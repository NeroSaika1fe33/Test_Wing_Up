using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionUI_csv : MonoBehaviour
{
    public CustomLists lists;
    public TMP_Text categoryText;
    public TMP_Text itemText;
    public PartsDataManager PDManager;

    List<string>[] items = {
        new List<string>(),
        new List<string>(),
        new List<string>()
    };

    //Changes
    string[] categories = { "Body", "Wheel", "Mainspring" };

    //player now select category(left/right)
    int currentCategory = 0;
    //select parts save
    int[] selected = { 0, 0, 0 };

    void Start()
    {
        PartsDataSet();
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
                selected[currentCategory] = items[currentCategory].Count - 1;
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            selected[currentCategory]++;
            if (selected[currentCategory] >= items[currentCategory].Count)
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

    }

    void ConfirmSelection()
    {
        GameSelectionData.body = selected[0];
        GameSelectionData.wheel = selected[1];
        GameSelectionData.wing = selected[2];

        SceneManager.LoadScene("Ingame_test");
    }

    void PartsDataSet()
    {
        for (int i = 0; i < categories.Length; i++)
        {
            for (int j = 0; j < PDManager.Number_of_Parts; j++)
            {
                if (PDManager.Get_PartsType(PDManager.Get_PartsName()[j]) == categories[i])
                {
                    Debug.Log(PDManager.Get_PartsName()[j]);
                    items[i].Add(PDManager.Get_PartsName()[j]);
                }
            }
        }
    }

    //データを次のシーンに持っていくためにCussomList(DDOL)に保存
    private void OnDestroy()
    {
        lists.DataStorage(items[0][selected[currentCategory]], items[1][selected[currentCategory]], items[2][selected[currentCategory]]);
    }
}
