using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomLists : MonoBehaviour
{
    public string[] scenename = { "Select" , "InGame" };
    string[] CustomList = new string[3];
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
    void OnSceneloaded(Scene scene, LoadSceneMode mode)
    {
        UnityEngine.Debug.Log(GetData()[0] + GetData()[1] + GetData()[2]);
    }
    void OnSceneUnloaded(Scene current)
    {
        //Debug.Log("OnSceneUnloaded: " + current);
        if (current.name == scenename[1])
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {

    }
    public void DataStorage(string Body,string Wheel, string Mainspring)
    {
        CustomList[0] = Body;
        CustomList[1] = Wheel;
        CustomList[2] = Mainspring;
    }
    public string[] GetData()
    {
        return CustomList;
    }
}
