using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText; // Reference to TextMeshPro component

    private void Start()
    {
        if (highScoreText != null)
        {
            int highScore =PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "Your highest scores: " + highScore;
        }
    }
}

