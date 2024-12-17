using UnityEngine;

public class DroppingPencil : MonoBehaviour
{
    private Rigidbody2D rb2D; // Reference to the Rigidbody2D component
    private Timer time;

    private void Awake()
    {
        time = GameObject.Find("Game Manager").GetComponent<Timer>(); 
    }
    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb2D = GetComponent<Rigidbody2D>();

        // Disable physics simulation at the start
        rb2D.simulated = false;

    }

    void Update()
    {
        // Press the Space key to activate physics (falling starts)
        if (time.currentTime <=0)
        {
            rb2D.simulated = true; // Activate Rigidbody2D physics simulation
            Debug.Log("Rigidbody2D activated! Object will start falling.");
        }
    }
}

