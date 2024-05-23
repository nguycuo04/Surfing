using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManaer : MonoBehaviour
{
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioClip waterCrash;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private PlayerController playerController; 
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioSource>();
        destroySound = GetComponent<AudioClip>();
        waterCrash = GetComponent<AudioClip>();
        coinSound = GetComponent<AudioClip>();
        playerController = GameObject.Find ("Player"). GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        PlaySoundEffects();
    }

    void PlaySoundEffects()
    {
        if (playerController.isOnSurface == true)
        {
            audioManager.PlayOneShot(waterCrash, 1.0f);
        }

        if (playerController.eatCoin == true)
        {
            audioManager.PlayOneShot(coinSound, 1.0f);
        }
        if (playerController.rocketDestroy == true)
        {
            audioManager.PlayOneShot(destroySound, 1.0f);
        }
    }

 
}
