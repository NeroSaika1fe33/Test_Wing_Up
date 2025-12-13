using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CarHealth : MonoBehaviour
{
    [Header("HP Ý’è")]
    public int maxHP = 3;                // max HP = 3
    public float invincibleTime = 2f;    // if hit wall get save time
    public string wallTag = "wall";      // get wall tag
    public CarHPUI hpUI; //conect UI
    public CarController carcontroll;
    public QTEController qteController;
    public GameObject qtePanel;

    public int currentHP;  //now hp
    bool isInvincible = false;
    float invincibleTimer = 0f;

    public bool IsInvincible => isInvincible;
    void Start()
    {
        currentHP = maxHP;
        Debug.Log("Start HP = " + currentHP);

        if (hpUI != null)
        {
            hpUI.UpdateHP(currentHP);
        }
    }


    void Update()
    {
        // save timer
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
                Debug.Log("Invincible end");
            }
        }
    }

    // if wall is normal CollideriIs Trigger offj
    void OnCollisionEnter(Collision other)
    {
        if (!isInvincible && other.gameObject.CompareTag("wall"))
        {
            TakeDamage(1);
        }
    }



    void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0; //make sure HP>0

        hpUI.UpdateHP(currentHP);
        Debug.Log("Hit wall! HP = " + currentHP);

        if (hpUI != null)
        {
            hpUI.UpdateHP(currentHP);
        }

        // save time on
        isInvincible = true;
        invincibleTimer = invincibleTime;

        if (currentHP <= 0)
        {
            CarCrash();
        }
    }

    void CarCrash()
    {
        carcontroll.canControl = false;
        qteController.Minigame();
        Debug.Log("Car is crash");

    }

    public void ResetHp()
    {

        currentHP = 3;
    }

}
