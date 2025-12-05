using UnityEngine;
using static UnityEngine.InputSystem.HID.HID;
public class goal_contact : MonoBehaviour
{
    public CarController carController;

    float Timer = 0;	//タイマー
	bool count = false;	//カウントするかどうか
	float Time1000 = 0;	//タイマーを整数にしたもの(誤差修正用)
	private void Update()
	{
		if (count)
		{
			Timer += Time.deltaTime;
			Time1000 = Timer * 1000;
			float rest = Time1000 % 1;
			Time1000 -= rest;
		}
	}
	public void start_count()
	{
		count = true;
	}
	public void stop_count()
	{
		count = false;
	}
	public short Get_Time_ms()//タイマーのミリ秒を返す
	{
		return (short)(Time1000 % 1000);
	}
	public short Get_Time_s()//タイマーの秒を返す
	{
		return (short)(Timer / 1);
	}
	public short Get_Time_m()//タイマーの分を返す
	{
		return (short)(Timer / 60);
	}
	public short Get_Time_h()//タイマーの時を返す
	{
		return (short)(Timer / 3600);
	}
	void OnTriggerEnter(Collider other)
    {
        var car_situation = other.GetComponent<car_situation>();
        var car_manager = other.GetComponent<car_manager>();
        if (car_manager != null && car_situation != null)
        {
            car_situation.steat_goal();
            car_manager.Set_GoalTime(Get_Time_m(), Get_Time_s(), Get_Time_ms());
            car_manager.Set_GoalCount(3f);
			carController.canControl = false;


        }
    }
}
