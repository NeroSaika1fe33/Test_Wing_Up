using UnityEngine;
using UnityEngine.SceneManagement;
public class car_manager : MonoBehaviour
{
    car_situation car_Situation;
    
    float SceneCount = 0;

    public struct GoalTime
    {
		public short m;
		public short s;
		public short ms;
	}
    GoalTime _GoalTime = new GoalTime();
	void Start()
    {

    }
    void Update()
    {
        if(SceneCount > 0)
        {
            SceneCount -= Time.deltaTime;
            if(SceneCount <= 0)
            {
                GameManager.Instance.LoadScene(SceneList.Result);
                SceneCount = 0;
            }
        }
    }
    public void Set_GoalTime(short m,short s,short ms)
    {
        _GoalTime.m = m;
		_GoalTime.s = s;
		_GoalTime.ms = ms;
	}
    public short Get_GoalTime_m()
    {
        return _GoalTime.m;
    }
	public short Get_GoalTime_s()
	{
		return _GoalTime.s;
	}
	public short Get_GoalTime_ms()
	{
		return _GoalTime.ms;
	}

	public void Set_GoalCount(float f)//ƒS[ƒ‹Œã‚ÉResult‚És‚­‚Ü‚Å‚ÌŽžŠÔ
    {
        SceneCount = f;
    }
}
