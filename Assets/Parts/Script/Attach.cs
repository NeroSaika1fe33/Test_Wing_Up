using UnityEngine;

public class Attach : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    public Vector3 attachOffset = new Vector3(0f, 0f, 2.0f);

    public KeyCode StartKeyCall = KeyCode.A;

    bool CanAttach = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(StartKeyCall))
        {
            AttachStart();
        }
    }

    void AttachStart()
    {
        objectA.SetParent(objectB);

        objectA.localPosition = attachOffset;
    }
}
