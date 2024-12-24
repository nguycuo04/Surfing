using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleOnButtonPress : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);  // Target scale
    public float duration = 0.5f;  // Duration of each scale animation
    private Vector3 originalScale;  // Store the original scale

    void Start()
    {
        // Save the original scale when the script starts
        originalScale = transform.localScale;
    }

    public void ScaleObject()
    {
        // Kill any existing scale animations on this transform to prevent stacking
        transform.DOKill();

        // Scale up to the target scale, then scale back to the original scale
        transform.DOScale(targetScale, duration).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, duration).SetEase(Ease.OutBounce);
            });
    }
}



