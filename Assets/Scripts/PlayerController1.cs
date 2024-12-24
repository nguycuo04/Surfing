using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of left/right movement
    public float jumpForce = 2f; // Height of the jump
    public Transform groundCheck; // Position to check if the player is grounded
    public float groundCheckRadius = 0.1f; // Radius for ground detection
    public float fallSpeed = 5f; // Speed at which the player falls
    private Rigidbody2D rigibody;

    [SerializeField] bool isGrounded;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        // Check if the player is "grounded" by detecting any collider below
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);
        // Move left or right based on button press
        // Move left or right based on button press

    }
    public void MoveLeft()
    {
        rigibody.velocity = new Vector2(-moveSpeed, rigibody.velocity.y);
    }

    public void MoveRight()
    {
        rigibody.velocity = new Vector2(moveSpeed, rigibody.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        // Simulate falling if the player is not grounded
        if (!isGrounded)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.fixedDeltaTime);
        }
    }
}
