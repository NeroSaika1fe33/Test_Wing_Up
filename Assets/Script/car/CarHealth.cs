using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CarHealth : MonoBehaviour
{
    [Header("HP 設定")]
    public int maxHP = 3;                // max HP = 3
    public float invincibleTime = 2f;    // 無敵時間
    public string wallTag = "wall";      // get wall tag
    public CarHPUI hpUI; //conect UI
    public CarController carcontroll;
    public QTEController qteController;
    public GameObject qtePanel;

    public int currentHP;  //現在のHP（計算用）
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
        //無敵Timer
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
    // 物理衝突が発生した瞬間に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        //無敵状態でなく、かつ衝突相手のタグが「wall」のときだけダメージ処理
        if (!isInvincible && other.gameObject.CompareTag("wall"))
        {
            // 壁に当たったら 1 ダメージ
            TakeDamage(1);
        }
    }



    void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0; //HP>0確保

        hpUI.UpdateHP(currentHP);
        Debug.Log("Hit wall! HP = " + currentHP);

        if (hpUI != null)
        {
            hpUI.UpdateHP(currentHP);
        }

        //無敵on
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
