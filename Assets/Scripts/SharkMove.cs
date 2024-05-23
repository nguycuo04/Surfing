using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMove : MonoBehaviour
{
    [SerializeField] private Vector2[] movePoints; 
    [SerializeField] private float moveDuration = 2.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameOverScript gameOverScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
        MoveTheShark(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MoveTheShark()
    {
        if (gameOverScript.gameOver == false )
        {
            rb.DOPath(movePoints, moveDuration, PathType.Linear).SetOptions(true).SetEase(Ease.Linear);
        }
    }
}
