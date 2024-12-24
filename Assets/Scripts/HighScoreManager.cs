using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreManager : MonoBehaviour
{
    private int highScore; 
    private CoinLogic coinLogic; 

    private void Start()
    {
        highScore = 0; 
        coinLogic = GetComponent<CoinLogic>();
    }

    private void Update()
    {
        SaveHighScore(coinLogic.CoinNumber);
    }

    public void SaveHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScoreFirstScene", highScore);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScoreFirstScene", 0);
    }
}

