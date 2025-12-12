using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnyKey : MonoBehaviour
{
    
    public string nextSceneName = "Select test";

    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}