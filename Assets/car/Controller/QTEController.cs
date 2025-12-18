using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Carを再起動するためのA/D連打
public class QTEController : MonoBehaviour
{
    [Header("inspector object")]
    public GameObject qtePanel;
    public Slider progressBar;
    public TMP_Text InfoText;
    public TMP_Text timerText;

    [SerializeField] CarHealth carHealth;
    public CarController carcontroll;
    public goal_contact Goal_Contact;

    int currentCount = 0;
    int targetCount = 20;

    bool isRunning = false;

    [Header("Start")]
    bool isStartGameQTE = false;
    public float startQTETime = 3f;
    public float timeLimit = 5f;
    float timer = 0f;


    void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;

        //カウントダウン
        if (isStartGameQTE && timerText != null)
        {
            if (timer > 0f)
            {
                int display = Mathf.CeilToInt(timer);   // 2.8 → 3, 1.2 → 2, 0.3 → 1
                if (display < 0) display = 0;
                timerText.text = display.ToString();
            }
            else
            {

                timerText.text = "GO!";
            }
        }

        //UI設定
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timer).ToString();
        }

        //再起動判定
        if (timer <= 0f)
        {
            if (currentCount >= targetCount)
                Success();
            else
                Fail();

            return;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            currentCount++;
            UpdateUI();

            if (currentCount >= targetCount)
            {
                Success();
            }
        }
    }

    //Game起動とUI制御
    public void Minigame()
    {
        Debug.Log("minigame start");
        isRunning = true;
        currentCount = 0;
        timer = isStartGameQTE ? startQTETime : 9999f;

        timer = timeLimit; //time reset

        UpdateUI();

        if (qtePanel != null)
            qtePanel.SetActive(true);

        if (InfoText != null) InfoText.text = "A D Key Press repeatedly!";

        if (isStartGameQTE && timerText != null)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = Mathf.CeilToInt(timer).ToString();   // 3
        }
    }
    //再起動成功の処理
    void Success()
    {

        isRunning = false;
        if (qtePanel != null)
            qtePanel.SetActive(false);
        if (timerText != null)
        {
            timerText.text = "";
            timerText.gameObject.SetActive(false);
        }
        Debug.Log("QTE Success!");
        Debug.Log(carcontroll.canControl);

        if (isStartGameQTE)
        {
            isStartGameQTE = false;

            if (carcontroll != null)
                carcontroll.OnStartQTESuccess();  // boost
            Goal_Contact.start_count();

            return;   // carhealth is not run
        }

        if (carHealth != null)
        {
            Debug.Log(carHealth.currentHP);
        }

        if (carcontroll.canControl == false)
        {
            carcontroll.canControl = true;
        }

        carHealth.ResetHp();

    }

    void UpdateUI()
    {
        if (progressBar != null)
        {
            progressBar.maxValue = targetCount;
            progressBar.value = currentCount;
        }
    }

    public void StartGameQTE()
    {
        isStartGameQTE = true;
        Minigame();
    }

    //再起動失敗の処理
    void Fail()
    {
        isRunning = false;
        if (qtePanel != null)
            qtePanel.SetActive(false);

        if (timerText != null)
        {
            timerText.text = "";
            timerText.gameObject.SetActive(false);
        }
        Debug.Log("QTE Fail!");

        if (isStartGameQTE)
        {
            isStartGameQTE = false;

            if (carcontroll != null)
                carcontroll.OnStartQTEFail();
            Goal_Contact.start_count();
        }
        else
        {

            if (carcontroll != null && !carcontroll.canControl)
                carcontroll.canControl = true;
        }
    }
}
