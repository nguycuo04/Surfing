using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowTheBoat : MonoBehaviour
{
    [SerializeField] private Transform playerController;
    [SerializeField] private Vector3 followDirection;
    [SerializeField] private float followSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        //playerController = GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        followDirection = (playerController.position - transform.position).normalized;
        followDirection.z = 0f;
        transform.Translate(followDirection * followSpeed * Time.deltaTime);
    }
}
