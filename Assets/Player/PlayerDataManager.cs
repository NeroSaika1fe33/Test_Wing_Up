using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    //シングルトンパターン
    public static PlayerDataManager Instance { get; private set; }


    public Player player { get; private set; }
    public string result { get; private set; }

    private string savePath => Path.Combine(Application.persistentDataPath, "playData.json");

    //保存するためのデータ
    [System.Serializable]
    private class PlayerSaveData
    {
        public Player player;
        public string result;
    }

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

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    public void SetMatchResult(string _result)
    {
        result = _result;
    }

    public void Save()
    {
        PlayerSaveData saveData = new PlayerSaveData
        {
            player = player,
            result = result
        };

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
            player = saveData.player;
            result = saveData.result;
            Debug.Log("プレーデータを読み込む");
        }
        else
        {
            Debug.Log("ファイルを見つからない");
        }
    }

    private void Update()
    {
    }

}