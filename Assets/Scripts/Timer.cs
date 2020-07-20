using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeLeft = 160;
    public TextMeshProUGUI timerText;

    void Update()
    {
        timerText.text = timeLeft.ToString("F2");
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0) TimerEnd();
    }

    void TimerEnd() => Debug.Log("Timer has over");
}