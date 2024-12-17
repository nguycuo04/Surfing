using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class BlinkEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToBlink; // Assign your text component here
    [SerializeField] float blinkDuration = 1.0f;  // Duration for each blink

    void Start()
    {
        if (textToBlink != null)
        {
            // Start blinking effect (loop infinitely)
            textToBlink.DOFade(0, blinkDuration).SetLoops(-1, LoopType.Yoyo);

            // Call the method to stop the effect after 10 seconds
            //Invoke("StopBlinkingAndDisappear", 2f);
        }
    }

    void StopBlinkingAndDisappear()
    {
        // Stop all DoTween animations on the text
        textToBlink.DOKill();

        // Make the text completely transparent
        textToBlink.DOFade(0, 0.5f).OnComplete(() =>
        {
            // Optionally deactivate or destroy the text object after fading out
            textToBlink.gameObject.SetActive(false);
        });
    }
}

