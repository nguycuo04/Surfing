using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public Transform fishingRodTip; // The starting point (e.g., fishing rod tip)
    public Transform hook;          // The end point (e.g., the hook or bait)

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Start and end point
    }

    void Update()
    {
        lineRenderer.SetPosition(0, fishingRodTip.position);
        // Add wave effect
        Vector3 waveOffset = new Vector3(0, Mathf.Sin(Time.time * 5f) * 0.1f, 0);
        lineRenderer.SetPosition(1, hook.position + waveOffset);
        // Update the line positions
        lineRenderer.SetPosition(0, fishingRodTip.position);
        lineRenderer.SetPosition(1, hook.position);

        // Optionally, add animation effects here (e.g., waving line)
    }
}

