using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class replybutton : MonoBehaviour
{

    public void playgame()
    {
        SceneManager.LoadScene("GameScreen");
    }
}