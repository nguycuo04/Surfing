using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBoxCollider : MonoBehaviour
{
    private GameOverScript gameOverScript;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
        boxCollider = GetComponent<BoxCollider2D>(); 

    }

    void Update()
    {
        if (gameOverScript.gameOver==true)
        {
            if (boxCollider.enabled)
            {
                boxCollider.enabled = false;
                Debug.Log("BoxCollider2D deactivated.");
            }
        }
    }
}
