using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float outOfBoundX = -10.0f;
    [SerializeField] private GameOverScript gameOverScript; 
    // Start is called before the first frame update
    void Start()
    {
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverScript.gameOver == false)
        {
            MoveWaveLeft();
            if (transform.position.x < outOfBoundX)
            {
                Destroy(gameObject);
            }
        }
      
    }
    void MoveWaveLeft()
    {
        transform.Translate(Vector2.left  * speed * Time.deltaTime); 
    }
}
