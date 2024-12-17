using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHookController : MonoBehaviour
{
    public Transform rodTip;           // The starting point of the fishing rod
    public LineRenderer lineRenderer;  // Reference to the LineRenderer for the fishing line
    public float swingSpeed = 2f;      // Speed of the swinging motion
    public float swingAngleRange = 45f; // Maximum angle to swing (in degrees)
    public float moveSpeed = 5f;       // Speed of the hook moving down or back
    public float maxDepth = 5f;        // Maximum distance the hook can travel down
    public float lineLength = 0.1f;      // Fixed length of the fishing line during swinging

    private bool isSwinging = true;    // State: Hook is swinging
    private bool isMovingOut = false; // State: Hook is moving out/down
    private bool isReturning = false; // State: Hook is returning to the rod tip
    private float currentAngle = 0f;  // Current angle of the hook swing
    private Vector3 direction;        // Direction vector for hook movement
    private Vector3 targetPosition;   // Position to move towards when returning

    void Start()
    {
        // Initialize LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }
    }

    void Update()
    {
        // Update the LineRenderer to connect the rod tip to the hook
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, rodTip.position); // Line start (rod tip)
            lineRenderer.SetPosition(1, transform.position); // Line end (hook)
        }

        // Handle the hook states
        if (isSwinging)
        {
            SwingHook();
            if (Input.GetKeyDown(KeyCode.Space)) // Start moving the hook when pressing Space
            {
                StartMovingOut();
            }
        }
        else if (isMovingOut)
        {
            MoveHookOut();
        }
        else if (isReturning)
        {
            ReturnHook();
        }
    }

    private void SwingHook()
    {
        // Oscillate the angle using sine wave
        currentAngle = Mathf.Sin(Time.time * swingSpeed) * swingAngleRange;
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // Calculate the position of the hook based on the angle and the fixed line length
        Vector3 offset = new Vector3(Mathf.Sin(angleInRadians), -Mathf.Cos(angleInRadians), 0) * lineLength;
        transform.position = rodTip.position + offset;

        // Store the direction for launching the hook
        direction = offset.normalized;
    }

    private void StartMovingOut()
    {
        isSwinging = false;
        isMovingOut = true;

        // Use the direction calculated during the swing for the hook's movement
        direction = direction.normalized;
    }

    private void MoveHookOut()
    {
        // Move the hook in the direction it was swinging
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Check if the hook has reached its max depth
        if (Vector3.Distance(transform.position, rodTip.position) >= maxDepth)
        {
            isMovingOut = false;
            isReturning = true;

            // Set target position as the rod tip for returning
            targetPosition = rodTip.position;
        }
    }

    private void ReturnHook()
    {
        // Smoothly move the hook back to the rod tip
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the hook is close enough to the rod tip, reset to swinging state
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isReturning = false;
            isSwinging = true;
        }
    }
}
