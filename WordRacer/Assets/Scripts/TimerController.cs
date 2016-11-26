using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Image TimerBar;
    public float seconds = 10.0f;
    public bool start_timer = true;
	
	void Update () {

        if (start_timer)
        {
            TimerBar.fillAmount -= 1.0f / seconds * Time.deltaTime;
            if (TimerBar.fillAmount <= 0.0f)
                TriggerTimeEnded();
        }
	}

    void TriggerTimeEnded()
    {
        GameObject.FindGameObjectWithTag("RoundsController").GetComponent<RoundsController>().loseCurrentRound();
    }

    public void resetTimer(float seconds)
    {
        this.seconds = seconds;
        TimerBar.fillAmount = 1.0f;
    }

    public void startTimer()
    {
        start_timer = true;
    }

    public void stopTimer()
    {
        start_timer = false;
    }

    public float getRemainingFill()
    {
        return TimerBar.fillAmount;
    }
}
