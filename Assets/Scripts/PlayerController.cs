using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float addForce = 1.0f;
    //[SerializeField] private Transform spriteRotate;
    [SerializeField] private float rotateDuration = 0.5f;
    [SerializeField] private GameObject waveEffect;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private Vector3 previousPos;
    [SerializeField] private Camera camPlayer;
    [SerializeField] public  bool isOnSurface = false;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float torqueForce;
    [SerializeField] public  bool eatCoin = false;
    [SerializeField] public bool rocketDestroy = false; 
    [SerializeField] private AudioClip waterCrash;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip rocketFireSound;
    [SerializeField] private AudioClip iceHit;
    [SerializeField] private AudioSource soudEffects;
    [SerializeField] public bool startGame = false;
    [SerializeField] private CoinLogic coinAddMore;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] public  bool moveNextLevel = false;
    

    // Start is called before the first frame update
    void Awake()
    {
        soudEffects = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = previousPos;
        startGame = true;
        coinAddMore = GameObject.Find("Game Manager").GetComponent<CoinLogic>();
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverScript.gameOver == false)
        {
            MoveRight();
            MoveCamera();

            if (Input.GetKeyDown(KeyCode.Space) && isOnSurface == true)
            {
                rb.AddForce(Vector2.up * speed * addForce, ForceMode2D.Impulse);
                //spriteRotate.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360);
                //spriteRotate.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 0), rotateDuration, RotateMode.FastBeyond360);
                isOnSurface = false;


            }

            if (Input.GetMouseButtonDown(0) && isOnSurface == false)
            {
                //float turn = Input.GetAxis("Horizontal"); 
                rb.AddTorque(rotationSpeed * torqueForce, ForceMode2D.Force);
            }

        }
       
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
            waveEffect.SetActive(true);
            isOnSurface = true;
            soudEffects.PlayOneShot(waterCrash);
        

        if (gameObject.CompareTag("Ice"))
        {
            soudEffects.PlayOneShot(iceHit);
        }

    }

    void MoveRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); 
    }
    void MoveCamera()
    {
        Vector3 offset = transform.position - previousPos;
        offset.z = 0;
        camPlayer.transform.position += offset;
        previousPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag== "Coin")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            Destroy(other.gameObject);
            coinAddMore.CoinAdd();
        }

        if (other.tag == "Rocket")
        {
            soudEffects.PlayOneShot(rocketFireSound);
            explosionEffect.SetActive(true);
            rocketDestroy = true;
            Destroy(other.gameObject);
           
        }

        if (other.tag == "Island")
        {
            
            moveNextLevel = true;
            nextLevelButton.SetActive(true); 
        }

    }


  
}
