using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
 
    [SerializeField] private int coinNumber =0;
    [SerializeField] private int coinIncrease; 
    [SerializeField] private TextMeshProUGUI coinNumberIndicator;
    [SerializeField] private GameOverScript gameOverScript; 
    // Start is called before the first frame update
    void Start()
    {
        //coinNumberIndicator = GetComponent<TextMeshProUGUI>();
        coinNumberIndicator.text = "Coin:" + 0;
        gameOverScript = GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void CoinAdd()
    { 
        if (gameOverScript.gameOver == false )
        {
            coinNumber += 1;
            coinNumberIndicator.text = "Coin:" + coinNumber.ToString();
        }
           
    }
}
