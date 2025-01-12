using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    [SerializeField] GameObject highScorePanel;
    private GameOverScript gameOverScript;
    public AudioClip buttonPressSound; // Assign the button press sound
    public float delayTime = 0.5f; // Adjust based on sound length

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
        SoundManager.Instance.PlaySound(buttonPressSound); // Play the sound
        StartCoroutine(TimeToDelaySound());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Load the next level
    public void LoadNextLevel()
    {
        SaveCurrentLevel();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SoundManager.Instance.PlaySound(buttonPressSound); // Play the sound
            StartCoroutine(TimeToDelaySound());
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load. This is the last level.");
        }
    }

    public void SaveCurrentLevel()
    {
        // Save the current scene's build index
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save(); // Ensure the data is written
        Debug.Log("Level Saved: " + SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound(buttonPressSound); // Play the sound
        StartCoroutine(TimeToDelaySound());
        //SaveCurrentLevel();
        Application.Quit(); 
    }

    public void StartGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            // Load the saved level
            int savedLevelIndex = PlayerPrefs.GetInt("SavedLevel");
            SoundManager.Instance.PlaySound(buttonPressSound); // Play the sound
            StartCoroutine(TimeToDelaySound());
            LoadSceneByIndex(savedLevelIndex + 1);
            Debug.Log("Loaded Saved Level: " + savedLevelIndex);
        }
        else
        {
            // Load the first level (index 0 or your desired starting level)
            SceneManager.LoadScene(1);
            Debug.Log("No Saved Level Found, Starting at Level 0");
        }
        
    }

    public void ViewTheHighScore()
    {
        SoundManager.Instance.PlaySound(buttonPressSound); // Play the sound
        StartCoroutine(TimeToDelaySound());
        highScorePanel.SetActive(true); 
    }

    private IEnumerator TimeToDelaySound()
    {
        yield return new WaitForSeconds(delayTime);
    }
}
