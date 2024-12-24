using UnityEngine;
using DG.Tweening; // Import DOTween namespace

public class ButtonFlyIn : MonoBehaviour
{
    public RectTransform buttonRect;  // Assign the button's RectTransform in the Inspector
    public Vector2 startPosition = new Vector2(1000, 0); // Off-screen start position
    public RectTransform endPos;      // RectTransform of the target position
    public float duration = 1f;       // Duration of the fly-in effect
    public Ease easing = Ease.OutBack; // Easing type for the animation

    void Start()
    {
        // Ensure endPos is assigned
        if (endPos == null)
        {
            Debug.LogError("End Position (endPos) is not assigned!");
            return;
        }

        // Get the end position from the RectTransform
        Vector2 endPosition = endPos.anchoredPosition;

        // Set the button's starting position
        buttonRect.anchoredPosition = startPosition;

        // Animate the button flying in
        buttonRect.DOAnchorPos(endPosition, duration).SetEase(easing);
    }
}


