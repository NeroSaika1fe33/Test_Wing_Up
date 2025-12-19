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
    public static GameManager Instance;

    public SceneList CurrentScene;

    private void Start()
    {

    }
    //ÉVÅ[ÉìëJà⁄îªíË
    public void LoadScene(SceneList _NextSceneName)
    {
        switch (_NextSceneName)
        {
            case SceneList.Title:
                SceneManager.LoadScene("Title");
                CurrentScene = _NextSceneName;
                break;
            case SceneList.Car_Selection:
                SceneManager.LoadScene("Selection");
                CurrentScene = _NextSceneName;
                break;
            case SceneList.In_Game:
                SceneManager.LoadScene("InGame");
                CurrentScene = _NextSceneName;
                break;
            case SceneList.Result:
                SceneManager.LoadScene("Result");
                CurrentScene = _NextSceneName;
                break;
            case SceneList.Ranking:
                SceneManager.LoadScene("Ranking");
                CurrentScene = _NextSceneName;
                break;
        }
    }

}
