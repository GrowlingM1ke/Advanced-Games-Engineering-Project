using System;
using System.Collections.Generic;


public class GeneticAlgorithm
{
    public List<Individual> Population { get; set; }
    public int Generation { get; private set; }
    public float BestFitness { get; private set; }
    public double[] BestGenes { get; private set; }
    private SaveLoad sv;
    private Random Rand;

    public double FitnessSum;

    public GeneticAlgorithm(int populationSize, int geneSize, Random rand)
    {
        Generation = 1;
        Population = new List<Individual>();
        this.Rand = rand;
        sv = new SaveLoad();

        if (Parameters.load)
        {
            if (Parameters.file.Contains("pop"))
            {
                PopulationToSave pop = sv.LoadPopulation(Parameters.file);
                Population = pop.Population;
                populationSize = pop.populationSize;
                geneSize = pop.hiddenLayers * pop.hiddenNodes + Parameters.outputs;
            }
            else
            {
                IndividualToSave ind = sv.LoadIndividual(Parameters.file);
                geneSize = ind.individual.genes.Length;
                Parameters.hiddenNodes = ind.hiddenNodes;
                Parameters.hiddenLayers = ind.hiddenLayers;
                Population.Add(ind.individual);

            }
        }

        while (Population.Count < populationSize)
        {
           Population.Add(new Individual(geneSize, rand));
        }

    }

    public void NewGeneration()
    {
        if (Population.Count <= 0)
            return;

        List<Individual> newPopulation = new List<Individual>();
        Parameters.killCommand = false;

        FitnessSum = 0;
        foreach(Individual ind in Population) {
            FitnessSum += ind.fitness;
        }
        while (newPopulation.Count < Parameters.populationSize)
        {
            Individual parent1 = ChooseParent();
            Individual parent2 = ChooseParent();
            Individual child = null;
            if (Rand.NextDouble() < Parameters.crossoverRate)
                child = parent1.Crossover(parent2);
            else
                child = parent1.Crossover(parent1);

            child.Mutate();

            newPopulation.Add(child);
        }

        Population = newPopulation;

        Generation++;
    }

    private Individual ChooseParent()
    {
        double randomNumber = (Rand.NextDouble() * FitnessSum);

        for (int i = 0; i < Population.Count; i++)
        {
            randomNumber -= Population[i].fitness;

            if (randomNumber <= 0)
                return Population[i];            
        }

        return null;
    }
}
