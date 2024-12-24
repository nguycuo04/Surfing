using UnityEngine;

public class DroppingPencil : MonoBehaviour
{
    private GameOverScript gameOverScript; 
    private BoxCollider2D boxCollider;
    public float fallSpeed = 5f; // Speed at which the pencil falls
    public float deactivateThreshold = -50f; // Y position to deactivate the object
    private Vector3 initialPosition; // To store the initial position of the pencil
    private bool initialColliderState; // To store the initial state of the BoxCollider

    private void Awake()
    {
        // Get the Timer component from the Game Manager
        gameOverScript = GameObject.Find("Game Manager").GetComponent<GameOverScript>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D component not found on the pencil.");
        }

        // Store the initial state
        initialPosition = transform.position;
        initialColliderState = boxCollider != null && boxCollider.enabled;
    }

    private void Update()
    {
        // Check if the timer has reached zero
        if (gameOverScript.gameOver == true)
        {
            // Disable the BoxCollider2D
            if (boxCollider != null && boxCollider.enabled)
            {
                boxCollider.enabled = false;
                Debug.Log("BoxCollider2D deactivated.");
            }

            // Move the pencil downward
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            // Deactivate the GameObject if it exceeds the threshold
            if (transform.position.y <= deactivateThreshold)
            {
                gameObject.SetActive(false);
                Debug.Log("Pencil deactivated after exceeding threshold.");
            }
        }
    }

    // Method to reset the pencil state
    public void ResetPencil()
    {
        // Reset the position
        transform.position = initialPosition;

        // Reset the BoxCollider2D state
        if (boxCollider != null)
        {
            boxCollider.enabled = initialColliderState;
        }

        // Reactivate the GameObject
        gameObject.SetActive(true);

        Debug.Log("Pencil reset to initial state.");
    }
}
