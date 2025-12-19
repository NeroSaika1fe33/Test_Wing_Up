using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomLists : MonoBehaviour
{
    SetParts setParts = null;
    GameObject Car = null;
    PartsDataManager PDM;
    public string scenename = "InGame_test";
    public string[] CustomList = new string[3];

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneloaded;
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneloaded(Scene scene, LoadSceneMode mode)
    {

        //Debug.Log("Sceneロード完了" + scene.name  + "" + scenename);
        if (scene.name == scenename)
        {
            var ob = GameObject.Find("PartsDataManager");
            PDM = ob.GetComponent<PartsDataManager>();
            Car = GameObject.Find("Car");
            setParts = Car.GetComponent<SetParts>();
            if (setParts != null) { Debug.Log("SetParts発見"); }
            //Debug.Log("InGame_testシーンの読み込み完了");
            Debug.Log(PDM.Get_PartsID(CustomList[0]));
            Debug.Log(PDM.Get_PartsID(CustomList[1]));
            Debug.Log(PDM.Get_PartsID(CustomList[2]));
            setParts.InitialSettingsParts(PDM.Get_PartsID(CustomList[0]), PDM.Get_PartsID(CustomList[1]), PDM.Get_PartsID(CustomList[2]));
        }
        //UnityEngine.Debug.Log(GetData()[0] + GetData()[1] + GetData()[2]);
    }

    void OnSceneUnloaded(Scene current)
    {
        //Debug.Log("OnSceneUnloaded: " + current);
        if (current.name == scenename)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {

    }
    public void DataStorage(string Body, string Wheel, string Mainspring)
    {
        CustomList[0] = Body;
        CustomList[1] = Mainspring;
        CustomList[2] = Wheel;
    }
    public string[] GetData()
    {
        return CustomList;
    }

}
