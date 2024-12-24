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
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float addForce = 1.0f;
    //[SerializeField] private Transform spriteRotate;
    [SerializeField] private GameObject waveEffect;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject nexLevelCongrats;
    [SerializeField] private GameObject nextLevelEffect;
    [SerializeField] private Vector3 previousPos;
    [SerializeField] private Camera camPlayer;
    [SerializeField] public  bool isOnSurface = false;
    //[SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float torqueForce;
    [SerializeField] public  bool eatCoin = false;
    [SerializeField] public bool rocketDestroy = false; 
    [SerializeField] private AudioClip waterCrash;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip rocketFireSound;
    [SerializeField] private AudioClip iceHit;
    [SerializeField] private AudioSource soudEffects;
    //[SerializeField] public bool startGame = false;
    [SerializeField] private CoinLogic coinAddMore;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] public  bool moveNextLevel = false;
    [SerializeField] private bool runAd = false;
    [SerializeField] private GameObject twoPoint;
    [SerializeField] private GameObject fourPoint;
    [SerializeField] private GameObject sixPoint;
    [SerializeField] private GameObject eightPoint;
    [SerializeField] private GameObject tenPoint;
    [SerializeField] private GameObject minusTwoPoint;
    [SerializeField] private GameObject minusFourPoint;
    [SerializeField] private GameObject minusSixPoint;
    [SerializeField] private GameObject multiplyTwoPoint;
    [SerializeField] private GameObject divideTwoPoint;
    [SerializeField] private GameObject multiplyFourPoint;
    [SerializeField] private GameObject divideFourPoint;
    [SerializeField] private float activeDuration = 2f; // Duration to stay active
    private bool isCurrentlyActive = false; // Flag to check if the object is active


    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(-5, 0, 0);
        soudEffects = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = previousPos;
        coinAddMore = GameObject.Find("Game Manager").GetComponent<CoinLogic>();
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverScript.gameOver == false)
        {
            //MoveRight();
            //MoveCamera();

            if (Input.GetMouseButton(0) && isOnSurface == true)
            {
                rb.AddForce(Vector2.up * speed * addForce, ForceMode2D.Impulse);
                isOnSurface = false;
            }

        }

        if (runAd == false && moveNextLevel ==true)
        {
            StartCoroutine(TimeDelayAd());
            runAd = true;
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

    //void MoveRight()
    //{
    //    transform.Translate(Vector3.right * speed * Time.deltaTime); 
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
            coinAddMore.CoinAdd(1);
        }

        if (other.tag == "+2")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(2);
            twoPoint.SetActive(true);
            ActivateObject(twoPoint);
            
        }

        if (other.tag == "+4")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(4);
            ActivateObject(fourPoint);
        }
        if (other.tag == "+6")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(6);
            ActivateObject(sixPoint);
        }
        if (other.tag == "+8")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(8);
            ActivateObject(eightPoint);
        }
        if (other.tag == "+10")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(10);
            ActivateObject(tenPoint);
        }
        if (other.tag == "-2")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(-2);
            ActivateObject(minusTwoPoint);
        }
        if (other.tag == "-4")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(-4);
            ActivateObject(minusFourPoint);
        }
        if (other.tag == "-6")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(-6);
            ActivateObject(minusSixPoint);
        }

        if (other.tag == "nhan2")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.MultiplyScore(2);
            ActivateObject(multiplyTwoPoint);
        }
        if (other.tag == "nhan4")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.MultiplyScore(4);
            ActivateObject(multiplyFourPoint);
        }

        if (other.tag == "chia2")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.DivideScore(2);
            ActivateObject(divideTwoPoint);
        }
        if (other.tag == "chia4")
        {
            soudEffects.PlayOneShot(coinSound);
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.MultiplyScore(4);
            ActivateObject(divideFourPoint);
        }


        //if (other.tag == "Rocket" || other.tag =="Atomicboom")
        //{
        //    soudEffects.PlayOneShot(rocketFireSound);
        //    explosionEffect.SetActive(true);
        //    rocketDestroy = true;
        //    Destroy(other.gameObject);

        //}

        if (other.tag == "Island" && gameOverScript.gameOver == false)
        {
            
            moveNextLevel = true;
            nextLevelButton.SetActive(true);
            nexLevelCongrats.SetActive(true);
            nextLevelEffect.SetActive(true);
        }
        if (other.tag == "Lake")
        {
            speed = 0;
            
        }
    }

    private void ActivateObject(GameObject targetObject)
    {
        isCurrentlyActive = true; // Set the flag to true
        targetObject.SetActive(true); // Activate the object

        // Deactivate the object after the specified duration
        Invoke(nameof(ResetFlagAndDeactivate), activeDuration);
        StartCoroutine(DeactivateObjectAfterTime(targetObject, activeDuration));
    }

    private IEnumerator DeactivateObjectAfterTime(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(false); // Deactivate the object
    }

    private void ResetFlagAndDeactivate()
    {
        isCurrentlyActive = false; // Reset the flag to allow reactivation
    }
    IEnumerator TimeDelayAd()
    {
        yield return new WaitForSeconds(0.5f);
    }

}
