using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Standby Phase Duration")]
    public float standbyDuration;

    [Header("Prep Phase Duration")]
    public float prepDuration;

    [Header("Battle Phase Duration")]
    public float battleDuration;


     

    // Start is called before the first frame update
    void Start()
    {
        //currentTime = 0;
    } 

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if(hasLimit && ((countDown && currentTime <= timerLimit)) || (!countDown && currentTime >= timerLimit))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
        }


        SetTimerText();
    }

    public void MethodFromTimer()
     {
          Debug.Log("Debug from timer script");
     }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }

    public void startPrep()
    {
        currentTime = prepDuration;
        timerText.color = Color.white;
    }

    public void startBattle()
    {
        currentTime = battleDuration;
        timerText.color = Color.green;
    }

    public void startStandby()
    {
        currentTime = standbyDuration;
        timerText.color = Color.blue;
    }

    public float getCurrentTime()
    {
        return currentTime;
    }


}
