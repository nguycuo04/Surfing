using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonsFadeIn : MonoBehaviour
{
    [System.Serializable]
    public class ButtonFadeData
    {
        public RectTransform buttonRect; // Button's RectTransform
        public float delay;             // Delay before fade-in starts
    }

    public ButtonFadeData[] buttons;   // Array to hold all buttons' fade data
    public float duration = 1f;        // Duration of the fade-in effect

    void Start()
    {
        FadeInButtons();
    }

    void FadeInButtons()
    {
        foreach (var buttonData in buttons)
        {
            if (buttonData.buttonRect == null)
            {
                Debug.LogError("Button RectTransform is not assigned!");
                continue;
            }

            // Get or add a CanvasGroup for controlling alpha
            CanvasGroup canvasGroup = buttonData.buttonRect.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = buttonData.buttonRect.gameObject.AddComponent<CanvasGroup>();
            }

            // Start with the button fully transparent
            canvasGroup.alpha = 0;

            // Animate the fade-in
            canvasGroup.DOFade(1, duration).SetDelay(buttonData.delay);
        }
    }
}
