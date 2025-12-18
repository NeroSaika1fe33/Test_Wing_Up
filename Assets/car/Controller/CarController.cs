using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CarController : MonoBehaviour
{
    [Header("Auto Move")]
    [Range(0f, 1f)]
    public float autoForwardInput = 0.4f;//auto power

    public GameObject qtePanel;

    [Header("Speed")]
    public float acceleration = 500f;
    public float turnSpeed = 3f;
    public float maxSpeed = 55f;
    public int energy = 1000;

    private Rigidbody rb;
    RigidbodyConstraints normalConstraints;


    float inputV;
    float inputH;
    [Header("Gravity")]
    public float airGravityMultiplier = 2f; // 2~6
    public float downforcePerSpeed = 0.3f; // 0.1~0.6
    public float maxDownforce = 10f;       // 5~20

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
    public string wallTag = "wall";             //unity tag
    public float wallKnockbackSpeed = 20f;      //knockback speed
    public float wallKnockbackUp = 2f;          //knock to wall change y
    public float wallKnockbackLockTime = 0.2f;  //can't control time
    bool isWallKnockback = false;
    float wallKnockbackTimer = 0f;

    public float steerSmooth = 6f;   // 6~12
    float smoothH = 0f;

    [Header("Include")]
    public CarHPUI HPUI; //conect UI
    public CarHealth carHealth; // conect carHealth
    public QTEController qteController; // conect qteController

    [Header("Start QTE")]
    public float startQTECountdown = 3f;   // gamestart counter
    public float startBoostSpeed = 40f;   //sucess speed

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 1.0f;
    [SerializeField] private bool isGrounded;
    public Transform groundFront;
    public Transform groundBack;
    bool wasGrounded;
    public float landingDamp = 0.3f;
    public float angularDampOnGround = 6f;

    [Header("Child")]
    [SerializeField] private bool groundedFront;
    [SerializeField] private bool groundedBack;

    [Header("Grip (Handling)")]
    [Range(0f, 1f)]
    public float lateralGrip = 0.2f;     //Grip 
    public float gripOnlyWhenGrounded = 1f; // 1=contact Ground = tureÅG

    [Header("Anti-Lift (Downforce)")]
    public float antiLiftForce = 3f;      // 3~10ÅC -transform.up * 5f
    public float maxDownforceSpeed = 50f;

    [Header("Steering Physics")]
    public float yawTorque = 5f;      // 10~60 a/d speed
    public float yawDamp = 4f;
    enum DriftState { None, Preparing, Drifting, Cooldown }
    DriftState driftState = DriftState.None;
    float driftTimer = 0f;

    public bool canControl = true;

    QTEController qteConTroller;

    [Header("Reset Car")]
    private float Timer = 0f;
    Vector3 carPosition;

    
    void Start()
    {


        //panel off
        if (qtePanel != null)
            qtePanel.SetActive(false);

        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.maxAngularVelocity = 20f;

        normalConstraints = rb.constraints;
        //Debug.Log("game start");

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
            carHealth = GetComponent<CarHealth>();

        // Apply selection data (example)
        acceleration += GameSelectionData.addAcceleration;
        maxSpeed += GameSelectionData.addMaxSpeed;


    }

    void Update()
    {
        if (!canControl) return;//hp = 0 -> car can"t  control

        //Update ground status every frame 
        CheckGround();

        if (isWallKnockback)
        {
            inputV = 0f;
            inputH = 0f;

            //time counter
            wallKnockbackTimer += Time.deltaTime;
            if (wallKnockbackTimer >= wallKnockbackLockTime)
            {
                isWallKnockback = false;
                //x unlock
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
            }
            return; //if HandleDriftInput is not work
        }

        if (isGrounded)
        {
            inputV = autoForwardInput;
        }
        else
        {
            inputV = 0f;    //can't contact Ground y=0                
        }


        inputH = Input.GetAxis("Horizontal"); // A/D
        smoothH = Mathf.Lerp(smoothH, inputH, steerSmooth * Time.deltaTime);

        HandleDriftInput();
    }

    void CheckGround()
    {
        groundedFront = RayIsGround(groundFront != null ? groundFront.position : (transform.position + transform.forward * 0.8f));
        groundedBack = RayIsGround(groundBack != null ? groundBack.position : (transform.position - transform.forward * 0.8f));


        isGrounded = groundedFront || groundedBack;



    }

    bool RayIsGround(Vector3 origin)
    {
        Vector3 dir = -transform.up;
        RaycastHit hit;

        bool hitSomething = Physics.Raycast(origin, dir, out hit, groundCheckDistance, groundLayer, QueryTriggerInteraction.Ignore);

        Debug.DrawRay(origin, dir * groundCheckDistance, hitSomething ? Color.green : Color.red);

        if (!hitSomething) return false;


        float angle = Vector3.Angle(hit.normal, Vector3.up);
        return angle <= 60f;
    }

    void FixedUpdate()
    {
        bool freezeY = (rb.constraints & RigidbodyConstraints.FreezeRotationY) != 0;



        if (!canControl) return;//hp = 0 -> car can"t  control

        if (!wasGrounded && isGrounded)
        {
            Vector3 v = rb.linearVelocity;
            if (v.y < 0f) v.y *= landingDamp;
            rb.linearVelocity = v;
        }

        if (isGrounded)
        {
            Vector3 av = rb.angularVelocity;

            av.x = Mathf.Lerp(av.x, 0f, angularDampOnGround * Time.fixedDeltaTime);
            av.z = Mathf.Lerp(av.z, 0f, angularDampOnGround * Time.fixedDeltaTime);

            rb.angularVelocity = av;
        }

        wasGrounded = isGrounded;


        UpdateDriftState(); //drift
                            //drift to change accel
        float speedMul = GetCurrentSpeedMultiplier(); //get statespeed to cal 

        //car speed
        //float currentSpeed = rb.linearVelocity.magnitude;  // liner=rb(x,y,z) magnitude= longspeed sqrt()->m/s
        Vector3 vel = rb.linearVelocity;
        float currentSpeed = vel.magnitude;

        //drift state max speed
        float currentMaxSpeed = maxSpeed * speedMul;
        //boost cal
        boostedSpeed = (new Vector3(20.0f, 20.0f, 20.0f)).magnitude * driftSpeed;

        if (currentSpeed < currentMaxSpeed)
        {
            rb.AddForce(transform.forward * inputV * acceleration * speedMul, ForceMode.Acceleration);
        }



        ApplyLateralGrip();

        float turnMul = (driftState == DriftState.Drifting) ? 1.2f : 1f;


        if (isGrounded)
        {

            float yawDegPerSec = turnSpeed * 120f * turnMul;
            float yawThisStep = smoothH * yawDegPerSec * Time.fixedDeltaTime;

            float speed = rb.linearVelocity.magnitude;
            float down = Mathf.Clamp(speed * downforcePerSpeed, 0f, maxDownforce);
            rb.AddForce(-transform.up * down, ForceMode.Acceleration);

            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yawThisStep, 0f));

            Vector3 av = rb.angularVelocity;
            av.y = 0f;
            rb.angularVelocity = av;
        }



        ApplyAntiLift();
        //Aerial gravity
        //Strengthening aerial gravity
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * airGravityMultiplier, ForceMode.Acceleration);
        }
    }

    void ApplyLateralGrip()
    {
        if (!isGrounded) return;

        Vector3 v = rb.linearVelocity;
        Vector3 localV = transform.InverseTransformDirection(v);

        localV.x *= lateralGrip;
        rb.linearVelocity = transform.TransformDirection(localV);
    }

    void ApplyAntiLift()
    {

        if (!groundedFront && !groundedBack) return;

        bool tilt = groundedFront ^ groundedBack;
        if (!tilt) return;


        float speed = rb.linearVelocity.magnitude;
        float k = Mathf.Clamp01(speed / Mathf.Max(1f, maxDownforceSpeed));

        rb.AddForce(-transform.up * antiLiftForce * k, ForceMode.Acceleration);
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
        Debug.Log("HIT WALL: " + collision.gameObject.name);

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
            //x lock
            rb.constraints |= RigidbodyConstraints.FreezeRotationX;
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