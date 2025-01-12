using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Import DOTween namespace

public class RippleEffectManager : MonoBehaviour
{
    public Image rippleImagePrefab; // Reference to the ripple effect image prefab
    public Canvas canvas; // Reference to the canvas
    public float maxScale = 3f; // Maximum scale size for the ripple
    public float duration = 0.5f; // Duration of the ripple effect
    public float fadeDuration = 0.3f; // Duration for fading the ripple effect

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect mouse click
        {
            Vector2 position = Input.mousePosition; // Get mouse click position
            CreateRippleEffect(position);
        }
        else if (Input.touchCount > 0) // Detect touch input (for mobile)
        {
            Vector2 position = Input.GetTouch(0).position; // Get touch position
            CreateRippleEffect(position);
        }
    }

    void CreateRippleEffect(Vector2 position)
    {
        // Instantiate the ripple image at the position of touch or mouse click
        Image ripple = Instantiate(rippleImagePrefab, canvas.transform);
        ripple.transform.position = position;

        // Set the initial scale to (0, 0)
        ripple.rectTransform.localScale = Vector3.zero;

        // Disable raycasting on the ripple to avoid blocking UI interaction
        CanvasGroup canvasGroup = ripple.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }

        // Animate the scaling using DOTween
        DOTween.To(() => ripple.rectTransform.localScale,
                   scale => ripple.rectTransform.localScale = scale,
                   new Vector3(maxScale, maxScale, 1),
                   duration)
               .OnKill(() =>
               {
                   Destroy(ripple.gameObject); // Destroy the ripple after animation ends
               });

        // Optionally, fade out the ripple effect using DOTween
        ripple.DOFade(0, fadeDuration).SetDelay(duration).OnKill(() =>
        {
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true; // Re-enable raycasting after ripple fades
            }
        });
    }
}
