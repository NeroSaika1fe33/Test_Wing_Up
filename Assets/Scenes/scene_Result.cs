using UnityEngine;
using UnityEngine.SceneManagement;
public class scene_Result : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	// Update is called once per frame
	public void OnClick()
	{
		SceneManager.LoadScene("Title");
		//Debug.Log("‰Ÿ‚³‚ê‚½!");  // ƒƒO‚ğo—Í
	}
}
