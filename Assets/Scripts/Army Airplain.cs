using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAirplain : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float followSpeed = 5.0f; 
    //[SerializeField] private float startDelay = 1.0f;
    //[SerializeField] private float delayInterval = 2.0f;
    [SerializeField] private Transform playerController;
    [SerializeField] private Vector3 followDirection;

    void Update()
    {
        // MoveAirPlainRight(); 
        AirForceChasing(); 
    }

    void MoveAirPlainRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void AirForceChasing()
    {
        followDirection = (playerController.position - transform.position).normalized;
        followDirection.z = 0f;
        followDirection.y = 0f; 
        transform.Translate(followDirection * followSpeed * Time.deltaTime);

    }
}
