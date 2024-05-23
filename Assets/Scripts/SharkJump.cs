using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkJump : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float jumpSpeed = 20.0f;
    [SerializeField] Rigidbody2D rb; 
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x - transform.position.x) < 2.0f)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
        
    }
}
