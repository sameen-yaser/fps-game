using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelbutton : MonoBehaviour
{
    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("menu");
    }
}
