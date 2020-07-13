﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeLeft = 160;
    public TextMeshProUGUI timerText;

    void Update()
    {
        timerText.text = timeLeft.ToString("F2");
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            Debug.Log("Timer has over");
        }
    }
}
