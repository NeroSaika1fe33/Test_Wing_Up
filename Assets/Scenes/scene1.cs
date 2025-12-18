using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scene1 : MonoBehaviour
{
    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        SceneManager.LoadScene("SampleScene_test");
       // Debug.Log("押された!");  // ログを出力
    }
}