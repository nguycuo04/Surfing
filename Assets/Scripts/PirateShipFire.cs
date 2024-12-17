using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PirateShipFire : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField ] private float startDelay =1.0f;
    [SerializeField] private float intervalDelay = 7.0f;
    //[SerializeField] private float xOutOfBound = -10.0f;
    [SerializeField] private GameOverScript gameOverScript; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireTheBoat", startDelay, intervalDelay);
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
        
    }

    void FireTheBoat()
    {
        if (gameOverScript.gameOver == false)
        {
            Instantiate(rocket, transform.position, transform.rotation);
        }
        
    }

  
}
