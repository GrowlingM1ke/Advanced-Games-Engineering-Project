using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Click : MonoBehaviour
{

    public void highscores()
    {
        SceneManager.LoadScene("highscores");
    }
    public void playGame()
    {
        Parameters.race = false;
        if (Parameters.load)
            SceneManager.LoadScene("Pick Menu");
        else if (Parameters.track == 1)
            SceneManager.LoadScene("Race Track 1");
        else if (Parameters.track == 2)
            SceneManager.LoadScene("Race Track 2");
        else if (Parameters.track == 3)
            SceneManager.LoadScene("Race Track 3");
        else if (Parameters.track == 4)
            SceneManager.LoadScene("Race Track 4");
        else if (Parameters.track == 5)
            SceneManager.LoadScene("Race Track 5");
    }

    public void raceMenu()
    {
        Parameters.race = true;
        SceneManager.LoadScene("Racing Menu");
    }

    public void options()
    {
        SceneManager.LoadScene("Options Menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
