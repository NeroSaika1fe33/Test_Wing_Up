using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CarController : MonoBehaviour
{
    [Header("Auto Move")]
    [Range(0f,1f)]
    public float autoForwardInput = 0.4f;//auto power

    public GameObject qtePanel;

    [Header("Speed")]
    public float acceleration = 500f; 
    public float turnSpeed = 3f; 
    public float maxSpeed = 55f;
    public int energy = 1000;

    private Rigidbody rb;

    float inputV;
    float inputH;

    [Header("Drift")]
    public KeyCode driftKey = KeyCode.E;
    public float driftMinSpeed = 30f;    

    public float prepareTime = 2f;    
    public float driftTime = 2f;    
    public float cooldownTime = 2f;

    public float prepareSpeed = 0.85f; 
    public float driftSpeed = 1.5f;  
    public float cooldownSpeed = 0.75f;
    public float boostedSpeed = 0f;

    [Header("Wall Knockback")]
    public string wallTag = "wall"; //unity tag
    public float wallKnockbackSpeed = 20f; //knockback speed
    public float wallKnockbackUp = 2f; //knock to wall change y
    public float wallKnockbackLockTime = 0.2f; //can't control time
    bool isWallKnockback = false;
    float wallKnockbackTimer = 0f;

    [Header("Include")]
    public CarHPUI HPUI; //conect UI
    public CarHealth carHealth; // conect carHealth
    public QTEController qteController; // conect qteController

    [Header("Start QTE")]
    public float startQTECountdown = 3f;   // gamestart counter
    public float startBoostSpeed = 40f;   //sucess speed
   

    enum DriftState { None, Preparing, Drifting, Cooldown }
    DriftState driftState = DriftState.None;
    float driftTimer = 0f;

    public bool canControl = true;

    QTEController qteConTroller;
    void Start()    
    {
        //panel off
        if (qtePanel != null)
            qtePanel.SetActive(false);

        rb = GetComponent<Rigidbody>();
        Debug.Log("game start");

        canControl = false;


        if (qteController != null)
        {
            qteController.StartGameQTE();
        }
        else
        {
            canControl = true;   // have not qte->startgame
        }



        if (carHealth == null)
        {
            carHealth = GetComponent<CarHealth>();
        }
        
       

    }

    void Update()
    {
        
        if (!canControl) return;//hp = 0 -> car can"t  control

        if (isWallKnockback)
        {
            inputV = 0f;
            inputH = 0f;

            //time counter
            wallKnockbackTimer += Time.deltaTime;
            if (wallKnockbackTimer >= wallKnockbackLockTime)
            {
                isWallKnockback = false;
            }
            return; //if HandleDriftInput is not work
        }

        inputV = autoForwardInput; //auto
        

        inputH = Input.GetAxis("Horizontal"); // A/D

        HandleDriftInput();
    }

    void FixedUpdate()
    {
        if (!canControl) return;//hp = 0 -> car can"t  control

        UpdateDriftState(); //drift
              //drift to change accel
        float speedMul = GetCurrentSpeedMultiplier(); //get statespeed to cal

              //car speed
        float currentSpeed = rb.linearVelocity.magnitude;  // liner=rb(x,y,z) magnitude= longspeed sqrt()->m/s

              //drift state max speed
        float currentMaxSpeed = maxSpeed * speedMul;
        //boost cal
        boostedSpeed = rb.linearVelocity.magnitude * driftSpeed; // return to gobal

        if (currentSpeed < currentMaxSpeed)
        {
            rb.AddForce(transform.forward * inputV * acceleration * speedMul * Time.fixedDeltaTime);
        }

        
        float turnMul = (driftState == DriftState.Drifting) ? 1.2f : 1f;
        transform.Rotate(0f, inputH * turnSpeed * turnMul, 0f);
    }

    
    void HandleDriftInput()
    {
        
        if (driftState != DriftState.None)
            return;

        float currentSpeed = rb.linearVelocity.magnitude;
        //change m/s->km/s
        float kmh = currentSpeed * 3.6f;

        if (Input.GetKeyDown(driftKey))
        {
            Debug.Log($"Speed: {kmh:F1} km/h | State: {driftState} | V={inputV} H={inputH}");
        }


        bool isAccelerating = inputV > 0.1f;         
        bool isTurning = Mathf.Abs(inputH) > 0.1f; 
        bool driftPressed = Input.GetKeyDown(driftKey);
        
        if (Input.GetKeyDown(driftKey))
        {
            Debug.Log($"Speed={currentSpeed}  accel={isAccelerating}  turn={isTurning}  pressed={driftPressed}");
        }

        if (currentSpeed >= driftMinSpeed && isAccelerating && isTurning && driftPressed)
        {
            driftState = DriftState.Preparing;
            driftTimer = 0f;
             Debug.Log("Drift prepare start");
        }
    }
    
    void UpdateDriftState()
    {
        if (driftState == DriftState.None)
            return;

        driftTimer += Time.deltaTime;

        switch (driftState)
        {
            case DriftState.Preparing:
                if (driftTimer >= prepareTime)
                {
                    driftState = DriftState.Drifting;
                    driftTimer = 0f;
                    Debug.Log("Drift start!");// Debug.Log("Drift start!");

                    //car boostup
                    Vector3 boostedVel = rb.linearVelocity.normalized * boostedSpeed;
                    
                    rb.linearVelocity = boostedVel;

                    Debug.Log("boosted speed = " + boostedVel.magnitude);
                }
                break;

            case DriftState.Drifting:
                if (driftTimer >= driftTime)
                {
                    driftState = DriftState.Cooldown;
                    driftTimer = 0f;
                    Debug.Log("Drift cooldown");// Debug.Log("Drift cooldown");
                }
                break;

            case DriftState.Cooldown:
                if (driftTimer >= cooldownTime)
                {
                    driftState = DriftState.None;
                    driftTimer = 0f;
                    Debug.Log("Drift end");// Debug.Log("Drift end");
                }
                break;
        }
    }

    
    float GetCurrentSpeedMultiplier()
    {
        switch (driftState)
        {
            case DriftState.Preparing:
                return prepareSpeed;   
            case DriftState.Drifting:
                return driftSpeed;     
            case DriftState.Cooldown:
                return cooldownSpeed;  
            default:
                return 1f;                
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            //invincible time will not knock back
            if (carHealth != null && carHealth.IsInvincible)
            {
                return;
            }

            ContactPoint contact = collision.GetContact(0);

            //world coordinate change to car locate
            Vector3 localHitPoint = transform.InverseTransformPoint(contact.point); //// localHitPoint.z > 0 car infront
                                                                                    // localHitPoint.z < 0 car back

            if (localHitPoint.z <= 0f)
            {
                return;
            }


            Vector3 knockDir = -transform.forward;
            knockDir.y = 0f;
            knockDir.Normalize();

            //speed to 0f
            rb.linearVelocity = Vector3.zero;

            //add behind and y speed
            Vector3 newVel = knockDir * wallKnockbackSpeed + Vector3.up * wallKnockbackUp;
            rb.linearVelocity = newVel;

            //car lock
            isWallKnockback = true;
            wallKnockbackTimer = 0f;
        }
    }

    

    public void OnStartQTESuccess()
    {
        canControl = true;

        Vector3 dir = transform.forward;
        dir.y = 0f;
        dir.Normalize();

        // set start speed
        rb.linearVelocity = dir * startBoostSpeed;
    }

    //if fail just gamestart
    public void OnStartQTEFail()
    {
        canControl = true;
       
    }
    
}