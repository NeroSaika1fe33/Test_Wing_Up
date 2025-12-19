using UnityEngine;
public class start_point : MonoBehaviour
{
    float c = 0;
	public goal_contact Goal_Contact;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        c = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if ( -1 <= c)
        {
             c -= Time.deltaTime; 
        }
        if(c <= 0)
        {
            Goal_Contact.start_count();

        }

	}
    private void OnTriggerStay(Collider other)
    {
        if ( c <= 0)
        {
            var start = other.GetComponent<car_manager>();
            var start_move = other.GetComponent<car_move>();
            //レーススタート時に実行
            if (start != null)
            {
				Goal_Contact.start_count();
                start_move.start();
                //UnityEngine.Debug.Log("test");
            }
        }
    }
}
