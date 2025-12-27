using UnityEngine;

public class Car_move : MonoBehaviour
{
    public bool movable = false;

    public float horizontal;
    public float vertical;

    //ステータス
    public float turnspeed = 2f;
    public float speed = 800f;
    public short Max_speed = 10;

    private Rigidbody rb;
    private Transform tf;
    private Animator anim;
    private Vector3 velocity;
    public void start()
    {
        movable = true;
    }
    float get_speed()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        return currentVelocity.magnitude;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { // FixedUpdateに修正
        //UnityEngine.Debug.Log("test");
        //入力
        float move = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");

        if (movable)
        {
            //前後移動
            if (get_speed() < Max_speed)
            {
                rb.AddForce(transform.forward * move * speed * Time.fixedDeltaTime);

            }
            //左右移動
            transform.Rotate(0, steer * turnspeed, 0);
        }


       //UnityEngine.Debug.Log(get_speed());
        

    }
}