using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirmation : MonoBehaviour
{

    public InputField mutation;
    public InputField crossover;
    public InputField population;
    public InputField nodes;
    public InputField layers;

    public void OnClick()
    {
        double mutationValue;
        bool isDouble = Double.TryParse(mutation.text, out mutationValue);
        if (isDouble)
        {
            if (mutationValue >= 0 && mutationValue <= 1)
                Parameters.mutationRate = mutationValue;
            else
                isDouble = false;
        }
        if (!isDouble)
        {
            mutation.text = Parameters.mutationRate.ToString();
        }

        double crossoverValue;
        isDouble = Double.TryParse(crossover.text, out crossoverValue);
        if (isDouble)
        {
            if (crossoverValue >= 0 && crossoverValue <= 1)
                Parameters.crossoverRate = crossoverValue;
            else
                isDouble = false;
        }
        if (!isDouble)
        {
            crossover.text = Parameters.crossoverRate.ToString();
        }

        int populationValue;
        bool isInt = int.TryParse(population.text, out populationValue);
        if (isInt)
        {
            if (populationValue >= 1 && populationValue <= 300)
            {
                Parameters.populationSize = populationValue;
                population.text = Parameters.populationSize.ToString();
            }

            else
                isInt = false;
        }
        if (!isInt)
        {
            population.text = Parameters.populationSize.ToString();
        }

        int nodesValue;
        isInt = int.TryParse(nodes.text, out nodesValue);
        if (isInt)
        {
            if (nodesValue >= 1 && nodesValue <= 10)
            {
                Parameters.hiddenNodes = nodesValue;
                nodes.text = Parameters.hiddenNodes.ToString();
            }

            else
                isInt = false;
        }
        if (!isInt)
        {
            nodes.text = Parameters.hiddenNodes.ToString();
        }

        int layerValue;
        isInt = int.TryParse(layers.text, out layerValue);
        if (isInt)
        {
            if (layerValue >= 1 && layerValue <= 10)
            {
                Parameters.hiddenLayers = layerValue;
                layers.text = Parameters.hiddenLayers.ToString();
            }

            else
                isInt = false;
        }
        if (!isInt)
        {
            layers.text = Parameters.hiddenLayers.ToString();
        }
    }
}
