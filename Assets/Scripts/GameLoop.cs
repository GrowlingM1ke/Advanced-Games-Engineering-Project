using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameLoop : MonoBehaviour
{
    static GeneticAlgorithm ga;
    bool geneticLoop;
    public GameObject car;
    public List<GameObject> cars;
    public Text text;
    private SaveLoad sl = new SaveLoad();
    public InputField name;
    public Individual individual;
    public Button button;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        if (Parameters.race)
        {
            button.enabled = false;
            name.enabled = false;
            time = -3.0f;
            if (Parameters.collisions)
            {
                Physics2D.IgnoreLayerCollision(8, 8, false);
            }
            for (int i = 0; i < 3; i++)
            {
                cars.Add((GameObject)Instantiate(car));
                cars[cars.Count - 1].SetActive(true);
                cars[cars.Count - 1].GetComponent<CarAI>().setIndividual(sl.LoadIndividual(Parameters.carsToLoad[i]).individual, i);
                cars[cars.Count - 1].GetComponent<CarAI>().name = sl.LoadIndividual(Parameters.carsToLoad[i]).name;
            }

            // Instatiate the player driven car
            cars.Add((GameObject)Instantiate(car));
            cars[cars.Count - 1].SetActive(true);
            cars[cars.Count - 1].GetComponent<CarAI>().playerControl = true;
            text.text = "Ready";
        }
        else
        {
            // First calculate how big the population will be
            int sizeOfGenome = 0;
            // Number of biases in an Individual
            sizeOfGenome += Parameters.hiddenLayers * Parameters.hiddenNodes + Parameters.outputs;
            // Add up the weights of hidden layer
            sizeOfGenome += (int)Mathf.Pow(Parameters.hiddenNodes, Parameters.hiddenLayers);
            // Add up weights between first layer and hidden
            sizeOfGenome += Parameters.inputs * Parameters.hiddenNodes;
            // Add up weights between hidden and last layer
            sizeOfGenome += Parameters.hiddenNodes * Parameters.outputs;

            System.Random rand = new System.Random();
            // Create the GA
            ga = new GeneticAlgorithm(Parameters.populationSize, sizeOfGenome, rand);
            geneticLoop = false;

            text.text = "Generation " + ga.Generation;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Parameters.listForHighScore.Count != 0)
            {
                sl.SaveHighscore(Parameters.listForHighScore);
                Parameters.listForHighScore.Clear();
            }
            SceneManager.LoadScene("Main Menu");
        }
        if (Parameters.race)
        {
            if (Input.GetKeyDown("r"))
            {
                for (int i = cars.Count - 1; i >= 0; i--)
                {
                    Destroy(cars[i]);
                    cars.RemoveAt(i);
                    Start();
                }
            }

            time += Time.deltaTime;
            if (time > 0.0f  && time < 5.0f)
                text.text = "Go";
            else if (time > -2.0f && time < 5.0f)
                text.text = "Steady";
        }
        else
        {
            if (Input.GetKeyDown("j"))
            {
                Parameters.display = !Parameters.display;
            }

            if (Input.GetKeyDown("k"))
            {
                Parameters.killCommand = !Parameters.killCommand;
            }

            if (!geneticLoop)
            {
                for (int i = 0; i < ga.Population.Count; i++)
                {
                    cars.Add((GameObject)Instantiate(car));
                    cars[cars.Count - 1].SetActive(true);
                    cars[cars.Count - 1].GetComponent<CarAI>().setIndividual(ga.Population[i], i);
                }
                geneticLoop = true;
            }
            else
            {
                if (cars.Count > 0)
                {
                    for (int i = cars.Count - 1; i >= 0; i--)
                    {
                        if (cars[i].GetComponent<CarAI>().isDone)
                        {
                            ga.Population[cars[i].GetComponent<CarAI>().id].fitness = cars[i].GetComponent<CarAI>().fitness * (cars[i].GetComponent<CarAI>().checkpoints / 4);
                            Destroy(cars[i]);
                            cars.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    sl.SaveCSV(ga.Population, ga.Generation);
                    ga.NewGeneration();
                    text.text = "Generation " + ga.Generation;
                    geneticLoop = false;
                }

            }
        }
    }

    public void OnPopClick()
    {
        if (!name.text.Equals(""))
        {
            if (button.GetComponentInChildren<Text>().text.Contains("Population"))
            {
                sl.SavePopulation(name.text, ga.Population);
                name.text = "";
            }
            else
            {
                sl.SaveIndividual(name.text, individual);
                name.text = "";
            }
            
        }

    }

    public void OnPopClickLoad()
    {
        PopulationToSave populationToSave = sl.LoadPopulation("test.bin");
        Start();
        ga.Population = populationToSave.Population;
    }

    public void updateIndividual(Individual individual)
    {
        this.individual = individual;
    }

}
