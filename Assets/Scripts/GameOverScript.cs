using UnityEngine;

public class GameOverScript : MonoBehaviour
{
  
    [SerializeField] public bool gameOver = false;
    [SerializeField] private Timer time;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject restartTheGameButton;
    [SerializeField] private GameObject player;
    [SerializeField] private CoinLogic coinLogic; 

    private Vector3 screenMin;
    private Vector3 screenMax;

    void Start()
    {
       
        coinLogic = GetComponent<CoinLogic>();
        time = GetComponent<Timer>();

        // Calculate screen bounds
        CalculateScreenBounds();
    }

    void Update()
    {
        // Game Over if time runs out
        if (time != null && time.currentTime <= 0 && (coinLogic.CoinNumber < coinLogic.YourNumber))
        {
            TriggerGameOver("Time's up!");
        }

        // Game Over if the player is out of bounds
        if (player != null && IsOutOfScreen(player.transform.position))
        {
            TriggerGameOver("Player is out of screen!");
        }
    }

    void CalculateScreenBounds()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        screenMin = screenBottomLeft;
        screenMax = screenTopRight;
    }

    bool IsOutOfScreen(Vector3 position)
    {
        return position.x < screenMin.x || position.x > screenMax.x ||
               position.y < screenMin.y || position.y > screenMax.y;
    }

    void TriggerGameOver(string reason)
    {
        if (!gameOver) // Ensure Game Over logic is triggered only once
        {
            gameOver = true;
            Debug.Log($"Game Over: {reason}");
            if (gameOverText != null) gameOverText.SetActive(true);
            if (restartTheGameButton != null) restartTheGameButton.SetActive(true);
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        time.currentTime = time.currentMin * 60; 
    }
    

}
