using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes

public class level1button : MonoBehaviour
{
    // Function to be called when the button is clicked
    public void LoadScene(string Level1)
    {
        SceneManager.LoadScene(Level1);
    }
}

