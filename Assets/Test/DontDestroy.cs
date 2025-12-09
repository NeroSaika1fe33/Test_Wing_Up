using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroy : MonoBehaviour
{
    public string unload_scenename = "Result";
    public string load_scenename = "Result";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneloaded;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnSceneloaded(Scene scene,LoadSceneMode mode)
    {
        transform.position = new Vector3(3,35,10);
        transform.localEulerAngles = new Vector3(0, -120, 0);
    }
    void OnSceneUnloaded(Scene current)
    {
        //Debug.Log("OnSceneUnloaded: " + current);
        if(current.name == unload_scenename)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        
    }
}
