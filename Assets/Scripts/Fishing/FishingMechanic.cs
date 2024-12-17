using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMechanic : MonoBehaviour
{
    public Rigidbody2D hookRigidbody; // Hook's Rigidbody2D
    public float swingForce = 5f;    // Force for swinging
    public float castForce = 10f;    // Force for casting
    //public LineRenderer lineRenderer;
    //public Transform rodTip;

    void Update()
    {
        //lineRenderer.SetPosition(0, rodTip.position);
        //lineRenderer.SetPosition(1, hookRigidbody.position);
        // Swing the hook
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hookRigidbody.AddForce(Vector2.left * swingForce);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hookRigidbody.AddForce(Vector2.right * swingForce);
        }

        // Cast the hook downward
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            hookRigidbody.AddForce(Vector2.down * castForce, ForceMode2D.Impulse);
        }
    }
}
