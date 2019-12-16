using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PopulationToSave
{
    public double mutationRate { get; set; }
    public double crossoverRate { get; set; }
    public int populationSize { get; set; }
    public int hiddenNodes { get; set; }
    public int hiddenLayers { get; set; }
    public string name { get; set; }
    public List<Individual> Population { get; set; }
}
