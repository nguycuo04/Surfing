using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
 
    [SerializeField] private int coinNumber =0;
    [SerializeField] private int coinIncrease;
    [SerializeField] private int yourNumber;
    [SerializeField] private TextMeshProUGUI coinNumberIndicator;
    [SerializeField] TextMeshProUGUI yourNumberText; 
    [SerializeField] private GameOverScript gameOverScript; 
    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScoreText(); 
        gameOverScript = GetComponent<GameOverScript>();
        yourNumber = Random.Range(20, 100);
        yourNumberText.text = yourNumber.ToString(); 
    }

    public void CoinAdd(int score)
    { 
        if (gameOverScript.gameOver == false )
        {
            coinNumber += score;
            coinNumberIndicator.text = "Coin:" + coinNumber.ToString();
            SaveHighScore(coinNumber);
        }
           
    }

    public void UpdateHighScoreText()
    {
        coinNumberIndicator.text = " " + GetHighScore();
    }
    private const string HighScoreKey = "HighScore";
    public void SaveHighScore(int coinNumber)
    {
        int currentHighScore = GetHighScore();
        if (coinNumber > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, coinNumber);
            PlayerPrefs.Save();
        }
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0); // Default to 0 if no high score is saved
    }
}
