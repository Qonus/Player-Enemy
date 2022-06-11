using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    float timerValue;
    void Awake()
    {
        gameObject.SetActive(DataHolder.SpeedRunMode);

        timerValue = DataHolder.timer;
        DataHolder.timerStop = false;
    }
    void Update()
    {
        if (!DataHolder.timerStop)
        {
            timerValue += Time.deltaTime;
            TimerText.text = (timerValue).ToString("F2");
        }
        else
        {
            DataHolder.timer = timerValue;
        }
    }
}
