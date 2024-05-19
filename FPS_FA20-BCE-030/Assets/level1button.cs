using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level1button : MonoBehaviour
{
    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }
}