using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIndexDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text levelDisplay; // Reference to the TextMeshPro component

    private void Start()
    {
        UpdateSceneIndex();
    }

    private void UpdateSceneIndex()
    {
        if (levelDisplay != null)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            levelDisplay.text = "Level " + sceneIndex;
        }
    }
}
