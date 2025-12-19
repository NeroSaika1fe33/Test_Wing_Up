using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    //シングルトンパターン
    public static PlayerDataManager Instance { get; private set; }

    //一人プレイに仮定する
    public string playerName = "Player1";
    public int playerID = 1;
    public string[] PartsName = new string[3];
    public string abilityName = "なし";

    private PlayerStats playerStats { get; set; }   
    //Player player { get; private set; }
    public string result { get; private set; }

    private string savePath => Path.Combine(Application.persistentDataPath, "playData.json");

    //保存するためのデータ
    //[System.Serializable]
    //private class PlayerSaveData
    //{
    //    public Player player;
    //    public string result;
    //}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void SetPlayer(PlayerStats _playerStats)
    {
        playerStats = _playerStats;
        for (int i = 0; i < PartsName.Length; i++)
        {
            PartsName[i] = _playerStats.parts[0].partsName;
        }
    }

    public void SetMatchResult(string _result)
    {
        result = _result;
    }

    public void Save()
    {
        PlayerSaveData saveData = new PlayerSaveData(
            playerName,
            playerID,
            PartsName,
            playerStats.maxSpeed,
            playerStats.acceleration,
            playerStats.weight,
            abilityName,
            result
            );

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("保存されたパース：" + savePath);
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(json);
            Debug.Log("プレーデータを読み込む");
        }
        else
        {
            Debug.Log("ファイルを見つからない");
        }
    }

    public void Register(PlayerStats _playerStats)
    {
        this.playerStats = _playerStats;
    }

    private void Update()
    {
    }

}