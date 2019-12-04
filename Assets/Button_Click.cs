using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Click : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Race Track 1");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
