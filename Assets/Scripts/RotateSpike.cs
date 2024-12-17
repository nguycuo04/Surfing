using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Ensure you have the DOTween namespace

public class SpikeRotation : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(0, 0, 360); // Rotate around the Y-axis
    public float duration = 1f; // Time it takes to complete one rotation

    void Start()
    {
        // Infinite rotation using DORotate
        transform.DORotate(rotationAxis, duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear) // Smooth, constant rotation
                 .SetLoops(-1, LoopType.Restart); // Infinite loop
    }
}

