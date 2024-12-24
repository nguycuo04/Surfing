using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToBlink;
    [SerializeField] float blinkDuration = 1.0f;
    [SerializeField] float stopBlinkingDuration = 30.0f;

    void Start()
    {
        if (textToBlink == null)
        {
            Debug.LogWarning("BlinkEffect: TextToBlink is not assigned.");
            return;
        }

        // Start blinking effect
        textToBlink.DOFade(0, blinkDuration).SetLoops(-1, LoopType.Yoyo);

        // Stop blinking after the defined duration
        StartCoroutine(StopBlinkingAfterDelay(stopBlinkingDuration));
    }

    IEnumerator StopBlinkingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopBlinkingAndDisappear();
    }

    void StopBlinkingAndDisappear()
    {
        textToBlink.DOKill();
        textToBlink.DOFade(0, 0.5f).OnComplete(() =>
        {
            textToBlink.gameObject.SetActive(false);
        });
    }
}
