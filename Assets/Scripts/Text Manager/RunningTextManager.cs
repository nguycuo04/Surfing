using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RunningTextManager : MonoBehaviour
{
    public TextMeshProUGUI runningText; // Reference to TextMeshPro text
    public string fullText = "Hi Friends, I felt the lesson today is boring, can we do something interesting?";
    public float typingSpeed = 0.05f;  // Time between each character

    private Coroutine typingCoroutine;

    private void Start()
    {
        StartRunningText(); 
    }
    public void StartRunningText()
    {
        // Stop any ongoing typing coroutine before starting a new one
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        runningText.text = ""; // Clear the text
        foreach (char c in fullText)
        {
            runningText.text += c; // Add one character at a time
            yield return new WaitForSeconds(typingSpeed); // Wait before the next character
        }
    }
}

