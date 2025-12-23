using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneList : int
{
    Title,
    Car_Selection,
    In_Game,
    Result,
    Ranking
}

public class GameManager : MonoBehaviour
{
    //シングルトン
    public static GameManager Instance;

    public SceneList CurrentScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        //Debug
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        CurrentScene = SceneList.Title;
    }
    //シーン遷移判定
    public void LoadScene(SceneList _NextSceneName)
    {
        switch (_NextSceneName)
        {
            case SceneList.Title:
                SceneManager.LoadScene("Title");
                break;
            case SceneList.Car_Selection:
                SceneManager.LoadScene("Selection");
                break;
            case SceneList.In_Game:
                SceneManager.LoadScene("InGame");
                break;
            case SceneList.Result:
                SceneManager.LoadScene("Result");
                break;
            case SceneList.Ranking:
                SceneManager.LoadScene("Ranking");
                break;
        }
        CurrentScene = _NextSceneName;
    }


    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log($"Scene Loaded: {scene.name}");
    //}

    private void Update()
    {
        if (Input.anyKeyDown && CurrentScene == SceneList.Title)
        {
            LoadScene(SceneList.Car_Selection);
        }

        if (Input.GetKeyDown(KeyCode.Return) && CurrentScene == SceneList.Car_Selection)
        {
            LoadScene(SceneList.In_Game);
        }

        if (Input.anyKeyDown && CurrentScene == SceneList.Result)
        {
            LoadScene(SceneList.Title);
            PlayerDataManager.Instance.Save();
        }
    }

    public SceneList GetCurrentScene()
    {
        return CurrentScene;
    }
}
