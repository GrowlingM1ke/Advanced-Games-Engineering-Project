using System;
using System.Collections.Generic;

public class Individual
{
    public double[] genes { get; private set; }
    public double fitness { get; set; }
    private Random rand;

    public Individual(int noOfGenes, Random rand, bool randomizeGene = true)
    {
        fitness = 0;
        this.rand = rand;
        genes = new double[noOfGenes];
        if (randomizeGene)
        {
            for (int i = 0; i < genes.Length; i++)
                genes[i] = rand.NextDouble() * (Parameters.maxValue - Parameters.minValue) + Parameters.minValue;
        }

    }

    public Individual(double[] genes)
    {
        this.genes = genes;
        fitness = 0;
    }

    public Individual Crossover(Individual ind)
    {
        Individual child = new Individual(genes.Length, rand, randomizeGene: false);

        for (int i = 0; i < child.genes.Length; i++)
        {
            child.genes[i] = rand.NextDouble() < 0.5 ? genes[i] : ind.genes[i];
        }

        return child;
    }

    public void Mutate()
    {
        for (int i = 0; i < genes.Length; i++)
        {
            if (rand.NextDouble() < Parameters.mutationRate)
            {
                genes[i] = rand.NextDouble() * (Parameters.maxValue - Parameters.minValue) + Parameters.minValue;
            }
        }
    }

    public double[] computeOutput(double[] inputs)
    {
        // First check if we have the expected number of inputs
        if (inputs.Length != Parameters.inputs)
            throw new ArgumentException("Mismatch between expected inputs and received inputs");

        // Index to chek on which gene we are
        int geneIndex = 0;

        // Input to first Hidden Layer
        inputs = computeLayer(inputs, ref geneIndex, Parameters.hiddenNodes);
        // Iterate through all the hidden layers
        for (int i = 0; i < Parameters.hiddenLayers - 1; i++)
        {
            inputs = computeLayer(inputs, ref geneIndex, Parameters.hiddenNodes);
        }
        // Input to output Layer
        inputs = computeLayer(inputs, ref geneIndex, Parameters.outputs);

        // final inputs are ouputs so return the result
        return inputs;

    }

    private double[] computeLayer(double[] inputs, ref int geneIndex, int outputNo)
    {
        // Create a list for the hidden layer input
        double[] outputValues = new double[outputNo];
        // Get the first layer sorted out
        for (int i = 0; i < inputs.Length; i++)
        {
            // Iterate through each output
            for (int j = 0; j < outputValues.Length; j++)
            {
                outputValues[j] += inputs[i] * genes[geneIndex++];
            }
        }

        // Now for each hidden input add bias and apply tangent/sigmoid function
        for (int i = 0; i < outputValues.Length; i++)
        {
            outputValues[i] += genes[geneIndex++];
            outputValues[i] = Math.Tanh(outputValues[i]);
        }

        return outputValues;
    }
}
