using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositioningExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionLocation; 
    // Start is called before the first frame update
    void Start()
    {
        explosionLocation = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = explosionLocation.transform.position; 
    }
}
