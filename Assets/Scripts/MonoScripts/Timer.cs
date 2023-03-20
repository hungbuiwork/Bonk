using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// Manages the time between phases in the round controller.
    /// </summary>
    [Header("Component")]
    [SerializeField]
    private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool countDown;

    [Header("Limit Settings")]
    [SerializeField] private bool hasLimit;
    [SerializeField] private float timerLimit;

    [Header("Standby Phase Duration")]
    [SerializeField] private float standbyDuration;

    [Header("Prep Phase Duration")]
    [SerializeField] private float prepDuration;

    [Header("Battle Phase Duration")]
    [SerializeField] private float battleDuration;

    private bool Frozen = false;


    public void Pause()
    {
        Frozen = true;
    }
    public void Resume()
    {
        Frozen = false;
    }
    void Update()
    {
        //TODO in future: Improve in future by using observers instead of checking every frame.
        //Sets the timer text, and updates the time
        if (!Frozen)
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        }

        //set color depending on how much time left
        if (currentTime < 10) { timerText.color = Color.red; }
        else if (currentTime < 20) { timerText.color = Color.yellow; }
        else { timerText.color = Color.green; }

        if (hasLimit && ((countDown && currentTime <= timerLimit)) || (!countDown && currentTime >= timerLimit))
        {
            currentTime = timerLimit;
            SetTimerText();
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

    public void StartPrep()
    {
        currentTime = prepDuration;
        Resume();
    }

    public void StartBattle()
    {
        currentTime = battleDuration;
        Resume();
    }

    public void StartStandby()
    {
        currentTime = standbyDuration;
        Resume();
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool CheckTimeOver()
    {
        if (currentTime <= timerLimit)
        {
            return true;
        }

        return false;
    }


}
