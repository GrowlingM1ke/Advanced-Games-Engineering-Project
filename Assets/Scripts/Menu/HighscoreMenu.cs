using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class HighscoreMenu : MonoBehaviour
{

    public Text text;
    public Dropdown dropdown;
    private SaveLoad sv = new SaveLoad();
    ListHighScoreData list;
    // Start is called before the first frame update
    void Start()
    {
        list = sv.LoadHighscore();
    }

    public void ValueChange()
    {
        text.text = "";
        if (dropdown.value != 0)
        {
            List<HighScoreData> prunedData = new List<HighScoreData>();
            foreach (HighScoreData h in list.list) {
                if (h.map == dropdown.value)
                    prunedData.Add(h);
            }

            prunedData.OrderBy(o => o.time);

            string textString = "";
            for (int i = 0; i < prunedData.Count; i++)
            {
                string name;
                if (prunedData[i].name == "")
                    name = "Player";
                else
                    name = prunedData[i].name;
                textString += (i + 1) + ". " + name + "(" + prunedData[i].time + " seconds)\n";
            }

            text.text = textString;
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
