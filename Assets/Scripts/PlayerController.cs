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
    [SerializeField] private GameObject nexLevelCongrats;
    [SerializeField] private GameObject nextLevelEffect;
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
    //[SerializeField] private float rotationSpeedOnAir = 2.0f;
    
    

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

            if (Input.GetMouseButton(0) && isOnSurface == true)
            {
                rb.AddForce(Vector2.up * speed * addForce, ForceMode2D.Impulse);
                //spriteRotate.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360);
                //spriteRotate.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 0), rotateDuration, RotateMode.FastBeyond360);
                isOnSurface = false;


            }

            //if (Input.GetMouseButtonDown(0) && isOnSurface == false)
            //{
            //    //float turn = Input.GetAxis("Horizontal"); 
            //    rb.AddTorque(rotationSpeed * torqueForce*Time.deltaTime, ForceMode2D.Force);
            //}

        }

        //RotateOnAir();
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

    //void RotateOnAir()
    //{
    //    float rotationInput = Input.GetAxis("Vertical");
    //    float rotation = rotationInput * rotationSpeedOnAir * Time.deltaTime;
    //    transform.Rotate(0, 0, rotation);
    //}
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

        if (other.tag == "Rocket" || other.tag =="Atomicboom")
        {
            soudEffects.PlayOneShot(rocketFireSound);
            explosionEffect.SetActive(true);
            rocketDestroy = true;
            Destroy(other.gameObject);
           
        }

        if (other.tag == "Island" && gameOverScript.gameOver == false)
        {
            
            moveNextLevel = true;
            nextLevelButton.SetActive(true);
            nexLevelCongrats.SetActive(true);
            nextLevelEffect.SetActive(true);
        }

    }


  
}
