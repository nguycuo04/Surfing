using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public  float currentTime;
    [SerializeField] private int currentMin;
    [SerializeField] private TextMeshProUGUI timeRemaining;
    [SerializeField] private bool timerIsActive = false;
    [SerializeField] private  PlayerController playerController;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] private GameObject firstMessage; 
    // Start is called before the first frame update
    void Start()
    {
        currentTime = currentMin * 60;
        timerIsActive = true;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameOverScript = GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.startGame == true && gameOverScript.gameOver == false && timerIsActive == true)
        {
            
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                timerIsActive = false;
            }

        }
       
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeRemaining.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

        if (time.Seconds == 56.0f)
        {
            StopFirstMessage();
        }

    }

    public void StartTimer()
    {
        timerIsActive = true;

    }

    void StopFirstMessage()
    {
            firstMessage.SetActive(false);
    }
}
