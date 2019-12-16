using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RacePicker : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    public Dropdown colour;
    private SaveLoad sv;
    public Toggle collisions;
    public Toggle wallDeath;
    private Hashtable listOfCars;

    // Start is called before the first frame update
    void Start()
    {
        collisions.isOn = Parameters.collisions;
        wallDeath.isOn = Parameters.wallDeath;


        List<Dropdown> listOfD = new List<Dropdown>();
        listOfCars = new Hashtable();
        listOfD.Add(dropdown1);
        listOfD.Add(dropdown2);
        listOfD.Add(dropdown3);
        sv = new SaveLoad();
        dropdown2.ClearOptions();
        bool alreadyDone = false;

        foreach (Dropdown d in listOfD)
        {
            d.options.Clear();
            d.options.Add(new Dropdown.OptionData() { text = "" });
            for (int i = 0; i < 21; i++)
            {
                if (File.Exists("car" + i + ".bin"))
                {
                    IndividualToSave ind = sv.LoadIndividual("car" + i + ".bin");
                    d.options.Add(new Dropdown.OptionData() { text = ind.name });
                    if (!alreadyDone)
                    {
                        listOfCars.Add(d.options.Count - 1, "car" + i + ".bin");
                    }
                }
            }
            alreadyDone = true;
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StartRace()
    {
        if (!checkIfEmpty())
        {
            Parameters.carsToLoad[0] = (string)listOfCars[dropdown1.value];
            Parameters.carsToLoad[1] = (string)listOfCars[dropdown2.value];
            Parameters.carsToLoad[2] = (string)listOfCars[dropdown3.value];
            Parameters.collisions = collisions.isOn;
            Parameters.wallDeath = wallDeath.isOn;
            Parameters.colour = colour.options[colour.value].text;
            if (Parameters.track == 1)
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
    }


    private bool checkIfEmpty()
    {
        bool empty = false;
        if (dropdown1.value == 0)
            empty = true;
        if (dropdown2.value == 0)
            empty = true;
        if (dropdown3.value == 0)
            empty = true;

        return empty;
    }
}
