using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnyKey : MonoBehaviour
{
    
    public string nextSceneName = "Select";

    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}