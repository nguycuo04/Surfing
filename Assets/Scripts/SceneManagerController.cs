using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    [SerializeField] GameObject highScorePanel;
    private GameOverScript gameOverScript;

    private void Start()
    {
        gameOverScript = GetComponent<GameOverScript>(); 
    }

    // Load a specific scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load a specific scene by index
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning($"Invalid scene index: {sceneIndex}. Make sure it is added in Build Settings.");
        }
    }

    // Reload the current scene
    public void ReloadCurrentScene()
    {
        gameOverScript.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Load the next level
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load. This is the last level.");
        }
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void StartGame()
    {
        LoadSceneByIndex(1);
    }

    public void ViewTheHighScore()
    {
        highScorePanel.SetActive(true); 
    }
}
