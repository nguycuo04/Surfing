using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] public bool gameOver = false;
    [SerializeField] private Timer time;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject restartTheGameButton;
    [SerializeField] private GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        time = GetComponent<Timer>();
        //player = GameObject.Find ("Player").GetComponent<GameObject>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.rocketDestroy == true)
        {
            gameOver = true; 
            Debug.Log ("Game Over");
            gameOverText.SetActive(true);
            restartTheGameButton.SetActive(true); 
        }

        if ( player.transform.rotation.eulerAngles.z > 70 && player.transform.rotation.eulerAngles.z < 290)
        {
            gameOver = true;
            Debug.Log("Game Over");
            gameOverText.SetActive(true);
            restartTheGameButton.SetActive(true);
        }
        if (time.currentTime <=0)
        {
            gameOver = true;
            Debug.Log("Game Over");
            gameOverText.SetActive(true);
            restartTheGameButton.SetActive(true);
        }
    }
}
