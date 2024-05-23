using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    [SerializeField] private GameObject waveSpawn;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Vector3 previousSpawnPos;
    [SerializeField] private Vector3 nextSpawnPos;
    [SerializeField] Transform offset;
    [SerializeField] Camera camView;
    // Start is called before the first frame update
    private void Awake()
    {
        InvokeRepeating("SpawnTheWave", spawnDelay , spawnInterval);
        nextSpawnPos = previousSpawnPos + offset.position;
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void SpawnTheWave()
    {

        Instantiate(waveSpawn, nextSpawnPos, gameObject.transform.rotation);
        nextSpawnPos += offset.position;

        if (transform.position.x < camView.transform.position.x - 10f)
            Destroy(gameObject);
    }
   
}
