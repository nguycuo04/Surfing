using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomExplode : MonoBehaviour
{
    [SerializeField] private GameObject explosionBoom;
    [SerializeField] private ParticleSystem explosiveEffect;
    // Start is called before the first frame update
    void Start()
    {
        //explosionBoom.SetActive(false);
        //explosionBoom = GetComponent<GameObject>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //explosionBoom.transform.position = transform.position;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            //explosionBoom.SetActive(true);
            explosiveEffect.Play();
            //Destroy(gameObject);
        
    }



    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Wave")
    //    {
    //        explosionBoom.SetActive(true);
    //        Destroy(gameObject);
    //    }
    //}






}
