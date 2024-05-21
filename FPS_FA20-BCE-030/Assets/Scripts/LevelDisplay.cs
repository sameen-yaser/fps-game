using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelDisplay : MonoBehaviour
{
    public Text levelText; // Reference to the UI Text component
    public float displayDuration = 3f; // Duration to display the text

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        levelText.text = "Level " + sceneName.Substring(sceneName.Length - 1); // Assumes your level names end with the level number
        StartCoroutine(DisplayLevelText());
    }

    private IEnumerator DisplayLevelText()
    {
        levelText.gameObject.SetActive(true); // Show the text
        yield return new WaitForSeconds(displayDuration); // Wait for the specified duration
        levelText.gameObject.SetActive(false); // Hide the text
    }
}
