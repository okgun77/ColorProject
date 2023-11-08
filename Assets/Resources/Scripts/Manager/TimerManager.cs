using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float TimeRemaining { get; private set; }
    public bool TimerIsRunning { get; private set; }

    public void StartTimer(float _duration)
    {
        TimeRemaining = _duration;
        TimerIsRunning = true;
    }

    public void StopTimer()
    {
        TimerIsRunning = false;
    }

    private void Update()
    {
        if (TimerIsRunning)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                GameManager.Instance.timeText.text = FormatTime(TimeRemaining);
            }
            else
            {
                TimeRemaining = 0;
                TimerIsRunning = false;
                GameManager.Instance.EndGame();
            }
        }
    }

    private string FormatTime(float _time)
    {
        // 시간을 분:초 형태로 포맷하여 반환
        int minutes = (int)(_time / 60);
        int seconds = (int)(_time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
