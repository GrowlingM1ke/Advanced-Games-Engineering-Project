using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Populate : MonoBehaviour
{
    public Dropdown dropdown;
    private Hashtable listOfCars;
    private Hashtable listOfPopulations;
    public Dropdown thisDropdown;
    private SaveLoad sv;

    void Start()
    {
        sv = new SaveLoad();
        listOfCars = new Hashtable();
        listOfPopulations = new Hashtable();
    }
    public void switchState()
    {
        if (dropdown.value != 0)
        {
            if (dropdown.value == 1)
            {
                listOfCars.Clear();
                thisDropdown.options.Clear();
                thisDropdown.options.Add(new Dropdown.OptionData() { text = "" });
                for (int i = 0; i < 21; i++)
                {
                    if (File.Exists("car"+i+".bin"))
                    {
                        IndividualToSave ind = sv.LoadIndividual("car" + i + ".bin");
                        thisDropdown.options.Add(new Dropdown.OptionData() { text = ind.name });
                        listOfCars.Add(thisDropdown.options.Count - 1, "car" + i + ".bin");
                    }
                }
            }
            else
            {
                listOfPopulations.Clear();
                thisDropdown.options.Clear();
                thisDropdown.options.Add(new Dropdown.OptionData() { text = "" });
                for (int i = 1; i < 21; i++)
                {
                    if (File.Exists("pop" + i + ".bin"))
                    {
                        PopulationToSave ind = sv.LoadPopulation("pop" + i + ".bin");
                        thisDropdown.options.Add(new Dropdown.OptionData() { text = ind.name });
                        listOfPopulations.Add(thisDropdown.options.Count - 1, "pop" + i + ".bin");
                    }
                }
            }
                
        }
        else
        {
            thisDropdown.options.Clear();
        }

    }

    public void onClick()
    {
        if (thisDropdown.value != 0 && dropdown.value != 0)
        {
            if (dropdown.value == 1)
            {
                Parameters.file = (string)listOfCars[thisDropdown.value];
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
            else
            {
                Parameters.file = (string)listOfPopulations[thisDropdown.value];
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
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
