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
    [SerializeField] private TextMeshProUGUI yourHighScore;
    [SerializeField] TextMeshProUGUI yourNumberText; 
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] GameObject nextButton;
    [SerializeField] bool nextLevel;
    private int currentHighScore;
    private AdManagerBanner banner;
    private AdManagerInterstitial interstitial;
    [SerializeField] bool runAd = false;

    public int CoinNumber => coinNumber;
    public int YourNumber => yourNumber;
    // Start is called before the first frame update
    void Start()
    {
        nextLevel = false; 
        currentHighScore = GetHighScore();
        UpdateHighScoreText(); 
        gameOverScript = GetComponent<GameOverScript>();
        //yourNumber = Random.Range(currentHighScore, currentHighScore + 8);
        YourRandomNumber();
        yourNumberText.text = "Target: " + yourNumber.ToString();
        banner = GameObject.Find("AdManager").GetComponent<AdManagerBanner>();
        interstitial = GameObject.Find("AdManager").GetComponent<AdManagerInterstitial>();
        banner.LoadAd();
    }
    private void Update()
    {
        NextLevelControl();

        if (runAd == false && nextLevel == true)
        {
            StartCoroutine(TimeDelayAd());
            runAd = true;
        }
    }

    void YourRandomNumber()
    {
        if(currentHighScore <10)
        {
            yourNumber = Random.Range(10, 16);
        }
        else
            yourNumber = Random.Range(currentHighScore, currentHighScore + 6);
    }
    IEnumerator TimeDelayAd()
    {
        yield return new WaitForSeconds(0.5f);
        interstitial.ShowInterstitialAd();
    }
    void NextLevelControl()
    {
        if (coinNumber> yourNumber || coinNumber == yourNumber)
        {
            nextLevel = true;
            nextButton.SetActive(true); 
        }
    }
    public void CoinAdd(int score)
    { 
        if (gameOverScript.gameOver == false )
        {
            coinNumber += score;
            coinNumberIndicator.text = "Point: " + coinNumber.ToString();
            SaveHighScore(coinNumber);
        }
           
    }
    public void DivideScore(int score)
    {
        if (gameOverScript.gameOver == false)
        {
            coinNumber /= score;
            coinNumberIndicator.text = "Point: " + coinNumber.ToString();
            SaveHighScore(coinNumber);
        }
    }
    public void MultiplyScore(int score)
    {
        if (gameOverScript.gameOver == false)
        {
            coinNumber *= score;
            coinNumberIndicator.text = "Point: " + coinNumber.ToString();
            SaveHighScore(coinNumber);
        }
    }

    public void UpdateHighScoreText()
    {
        yourHighScore.text = "High: " + GetHighScore();
    }
    private const string HighScoreKey = "HighScore";
    public void SaveHighScore(int coinNumber)
    {
        currentHighScore = GetHighScore();
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
