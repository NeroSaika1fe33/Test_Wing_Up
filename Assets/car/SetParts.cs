using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetParts : MonoBehaviour
{
    public PartsDataManager partsDataManager;
    public string BodyPrefabName;
    public string TirePrefabName;
    public string WingPrefabName;

    public List<Transform> Installation_Location_Body = new List<Transform>();
    public List<Transform> Installation_Location_Tire = new List<Transform>();
    public List<Transform> Installation_Location_Wing = new List<Transform>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Aをi番目の場所に配置(パーツタイプは自動判別)
        PartsArrangement(BodyPrefabName, Installation_Location_Body[0]);
        PartsArrangement(TirePrefabName, Installation_Location_Tire[0]);
        PartsArrangement(TirePrefabName, Installation_Location_Tire[1]);
        PartsArrangement(WingPrefabName, Installation_Location_Wing[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PartsArrangement(string PartsName, Transform Installation_Location)
    {
        //パーツタイプ判別(未使用)
        string PartsType = partsDataManager.Get_PartsType(PartsName);
        // コード上では拡張子を付けない
        GameObject prefab = Resources.Load<GameObject>(PartsName);

        if (prefab == null)
        {
            Debug.LogError("Prefabが見つかりません: " + PartsName);
        }
        GameObject childObject = Instantiate(prefab, Installation_Location);

        childObject.transform.localPosition = new Vector3(0, 0, 0);
        childObject.transform.localRotation = Quaternion.identity;
        childObject.transform.localScale = new Vector3(1, 1, 1);

    }

    //パーツ名更新とプリハブ更新
    public void UpdateBodyParts(string PartsName)
    {
        BodyPrefabName = PartsName;
        Destroy(Installation_Location_Body[0].GetChild(0).gameObject);
        PartsArrangement(BodyPrefabName, Installation_Location_Body[0]);
    }
    public void UpdateTireParts(string PartsName)
    {
        TirePrefabName = PartsName;
        Destroy(Installation_Location_Tire[0].GetChild(0).gameObject);
        Destroy(Installation_Location_Tire[1].GetChild(0).gameObject);
        PartsArrangement(TirePrefabName, Installation_Location_Tire[0]);
        PartsArrangement(TirePrefabName, Installation_Location_Tire[1]);
    }
    public void UpdateWingParts(string PartsName)
    {
        WingPrefabName = PartsName;
        Destroy(Installation_Location_Wing[0].GetChild(0).gameObject);
        PartsArrangement(WingPrefabName, Installation_Location_Wing[0]);
    }
}
