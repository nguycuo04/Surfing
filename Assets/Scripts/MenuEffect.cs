using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEffect : MonoBehaviour
{
    [SerializeField] CanvasGroup panelCanvasGroup;  // To control fade effect
    [SerializeField] RectTransform panelTransform;  // To control scaling or sliding
    [SerializeField] GameObject highScorePanel; 

    private void Start()
    {
        // Initialize panel as hidden (optional)
        panelCanvasGroup.alpha = 0;
        panelTransform.localScale = Vector3.zero; // Start at 0 scale
                                                  // Create a sequence for combined animations
        Sequence openSequence = DOTween.Sequence();

        // Add scaling and fading animations to the sequence
        openSequence.Append(panelTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));  // Scale up to normal size
        openSequence.Join(panelCanvasGroup.DOFade(1, 0.5f));  // Fade in to full opacity

        openSequence.Play();
    }

    public void CloseThePanel()
    {
        highScorePanel.SetActive(false);
    }

}


