using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public  float currentTime;
    [SerializeField] public int currentMin;
    [SerializeField] private TextMeshProUGUI timeRemaining;
    [SerializeField] public bool timerIsActive = false;
    [SerializeField] private  PlayerController playerController;
    void Start()
    {
        currentTime = currentMin * 60;
        timerIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive == true)
        {
            
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                timerIsActive = false;
            }

        }
       
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeRemaining.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        timerIsActive = true;
        currentTime = currentMin * 60;

    }
}
