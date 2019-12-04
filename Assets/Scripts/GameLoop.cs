using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    static GeneticAlgorithm ga;
    bool geneticLoop;
    public GameObject car;
    public List<GameObject> cars;
    public Text text;
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (!geneticLoop)
        {
            for (int i = 0; i < ga.Population.Count; i++)
            {
                cars.Add((GameObject)Instantiate(car));
                cars[cars.Count-1].SetActive(true);
                cars[cars.Count-1].GetComponent<CarAI>().setIndividual(ga.Population[i], i);
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
                ga.NewGeneration();
                text.text = "Generation " + ga.Generation;
                geneticLoop = false;
            }

        }
    }

}
