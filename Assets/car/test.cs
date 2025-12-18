using UnityEngine;

public class test : MonoBehaviour
{
    public bool ablilty1 = false;
    struct Parts 
    {
        public int speed;
        public int max_speed;
    }
    Parts parts1 = new Parts();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ability1()
    {
        bool ablilty1 = true;
    }
}
