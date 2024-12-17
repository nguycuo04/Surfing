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
    private float speed = 6.0f;
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
    [SerializeField] public bool startGame = false;
    [SerializeField] private CoinLogic coinAddMore;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] public  bool moveNextLevel = false;
    [SerializeField] AdManagerBanner adManagerBanner;
    [SerializeField] AdManagerInterstitial interstitialAd;
    [SerializeField] private bool runAd = false;
    [SerializeField] private GameObject twoPoint;
    [SerializeField] private GameObject fourPoint;
    [SerializeField] private float activeDuration = 2f; // Duration to stay active

    private bool isCurrentlyActive = false; // Flag to check if the object is active

    // Start is called before the first frame update
    void Awake()
    {
        soudEffects = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = previousPos;
        startGame = true;
        coinAddMore = GameObject.Find("Game Manager").GetComponent<CoinLogic>();
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
        adManagerBanner = GameObject.Find("Ad Manager").GetComponent<AdManagerBanner>();
        interstitialAd = GameObject.Find("Ad Manager").GetComponent<AdManagerInterstitial>();
        adManagerBanner.LoadAd();
       
    }
    private void Start()
    {
        speed = 6.0f;
       
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
        soudEffects.PlayOneShot(coinSound);
        if (other.tag== "Coin")
        {
            eatCoin = true;
            Destroy(other.gameObject);
            coinAddMore.CoinAdd(1);
        }

        if (other.tag == "+2")
        {
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(2);
            twoPoint.SetActive(true);
            ActivateObject(twoPoint);
            
        }

        if (other.tag == "+4")
        {
            eatCoin = true;
            other.gameObject.SetActive(false);
            coinAddMore.CoinAdd(4);
            ActivateObject(fourPoint);
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
        interstitialAd.ShowInterstitialAd();
    }

}
