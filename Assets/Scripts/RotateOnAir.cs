using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeedOnAir = 2.0f;
    [SerializeField] private PlayerController controller; 
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isOnSurface ==false )
        {
            RotateOnAir();
        }
       
    }

    void RotateOnAir()
    {
        float rotationInput = Input.GetAxis("Vertical");
        float rotation = rotationInput * rotationSpeedOnAir * Time.deltaTime;
        transform.Rotate(0, 0, rotation);
    }
}
