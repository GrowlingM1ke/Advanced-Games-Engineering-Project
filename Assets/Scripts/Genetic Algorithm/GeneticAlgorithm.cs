using System;
using System.Collections.Generic;


public class GeneticAlgorithm
{
    public List<Individual> Population { get; private set; }
    public int Generation { get; private set; }
    public float BestFitness { get; private set; }
    public double[] BestGenes { get; private set; }

    private Random Rand;

    public double FitnessSum;

    public GeneticAlgorithm(int populationSize, int geneSize, Random rand)
    {
        Generation = 1;
        Population = new List<Individual>();
        this.Rand = rand;

        for (int i = 0; i < populationSize; i++)
        {
            Population.Add(new Individual(geneSize, rand));
        }

    }

    public void NewGeneration()
    {
        if (Population.Count <= 0)
            return;

        List<Individual> newPopulation = new List<Individual>();

        FitnessSum = 0;
        foreach(Individual ind in Population) {
            FitnessSum += ind.fitness;
        }
        while (newPopulation.Count < Parameters.populationSize)
        {
            Individual parent1 = ChooseParent();
            Individual parent2 = ChooseParent();

            Individual child = parent1.Crossover(parent2);

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
            if (randomNumber < Population[i].fitness)
                return Population[i];

            randomNumber -= Population[i].fitness;
        }

        return null;
    }
}
