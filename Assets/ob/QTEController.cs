using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QTEController : MonoBehaviour
{
    public goal_contact Goal_Contact;
    [Header("inspector object")]
    public GameObject qtePanel;
    public Slider progressBar;
    public TMP_Text InfoText;
    public TMP_Text timerText;


    public CarController carcontroll;

    int currentCount = 0;
    int targetCount = 20;

    bool isRunning = false;

    [Header("Start")]
    bool isStartGameQTE = false;
    public float startQTETime = 3f;
    public float timeLimit = 5f;
    float timer = 0f;

    [SerializeField] CarHealth carHealth;
   

    void Start()
    {
           
    }

void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;

        //3 2 1 GO
        if (isStartGameQTE && timerText != null)
        {
            if (timer > 0f)
            {
                int display = Mathf.CeilToInt(timer);   // 2.8 Å® 3, 1.2 Å® 2, 0.3 Å® 1
                if (display < 0) display = 0;
                timerText.text = display.ToString();
            }
            else
            {
              
                timerText.text = "GO!";
            }
        }


        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timer).ToString(); // 3,2,1 
        }

        //timeup judge
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

        // if is start qte
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
