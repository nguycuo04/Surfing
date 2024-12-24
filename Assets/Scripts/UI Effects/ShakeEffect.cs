using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonShakeWithRotation : MonoBehaviour
{
    private RectTransform buttonRect; // The button's RectTransform
    private Tween shakeTween; // Reference to the shake tween for control

    [Header("Shake Settings")]
    public float shakeAmount = 10f; // Horizontal shake distance
    public float rotationAngle = 10f; // Rotation angle (degrees)
    public float shakeSpeed = 0.2f; // Duration for one shake cycle

    void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        ToggleShake();
    }

    public void StartShaking()
    {
        if (shakeTween == null || !shakeTween.IsActive())
        {
            // Create a sequence for combined shake and rotation
            shakeTween = DOTween.Sequence()
                // Move left and rotate left
                .Append(buttonRect.DOAnchorPosX(buttonRect.anchoredPosition.x - shakeAmount, shakeSpeed).SetEase(Ease.InOutSine))
                .Join(buttonRect.DORotate(new Vector3(0, 0, rotationAngle), shakeSpeed).SetEase(Ease.InOutSine))
                // Move right and rotate right
                .Append(buttonRect.DOAnchorPosX(buttonRect.anchoredPosition.x + shakeAmount, shakeSpeed).SetEase(Ease.InOutSine))
                .Join(buttonRect.DORotate(new Vector3(0, 0, -rotationAngle), shakeSpeed).SetEase(Ease.InOutSine))
                .SetLoops(-1, LoopType.Yoyo); // Loop indefinitely with Yoyo effect
        }
    }

    public void StopShaking()
    {
        if (shakeTween != null)
        {
            shakeTween.Kill(); // Stop the tween
            buttonRect.anchoredPosition = Vector2.zero; // Reset the button's position
            buttonRect.rotation = Quaternion.identity; // Reset the button's rotation
        }
    }

    public void ToggleShake()
    {
        if (shakeTween != null && shakeTween.IsPlaying())
        {
            StopShaking();
        }
        else
        {
            StartShaking();
        }
    }
}
