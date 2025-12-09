using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetParts : MonoBehaviour
{
    public PartsDataManager partsDataManager;
    public List<Transform> Installation_Location_Tire = new List<Transform>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Aをi番目の場所に配置(パーツタイプは自動判別)
        int i = 0;
        PartsArrangement("A", Installation_Location_Tire[i]);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PartsArrangement(string PartsbName,Transform Installation_Location)
    {
        //パーツタイプ判別(未使用)
        string PartsType = partsDataManager.Get_PartsType(PartsbName);
        // コード上では拡張子を付けない
        GameObject prefab = Resources.Load<GameObject>(PartsbName);

        if (prefab == null)
        {
            Debug.LogError("Prefabが見つかりません: " + PartsbName);
        }
        GameObject childObject = Instantiate(prefab, Installation_Location);

        childObject.transform.localPosition = new Vector3(0, 0, 0);
        childObject.transform.localRotation = Quaternion.identity;
        childObject.transform.localScale = new Vector3(1, 1, 1);
    }

}
