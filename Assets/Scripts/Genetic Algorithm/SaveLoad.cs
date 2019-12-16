using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad
{
    public void SaveIndividual(string name, Individual individual)
    {
        IndividualToSave i = new IndividualToSave();
        i.mutationRate = Parameters.mutationRate;
        i.crossoverRate = Parameters.crossoverRate;
        i.populationSize = Parameters.populationSize;
        i.hiddenNodes = Parameters.hiddenNodes;
        i.hiddenLayers = Parameters.hiddenLayers;
        i.individual = individual;
        i.name = name;

        string fileName = null;
        for (int j = 1; j < 21; j++)
        {
            fileName = "car" + j + ".bin";
            if (!File.Exists(fileName))
                break;
            else if (j == 20)
                File.Delete(fileName);
        }


        FileStream stream = File.Create(fileName);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, i);
        stream.Close();
    }

    public void SavePopulation(string name, List<Individual> population)
    {
        PopulationToSave i = new PopulationToSave();
        i.mutationRate = Parameters.mutationRate;
        i.crossoverRate = Parameters.crossoverRate;
        i.populationSize = Parameters.populationSize;
        i.hiddenNodes = Parameters.hiddenNodes;
        i.hiddenLayers = Parameters.hiddenLayers;
        i.Population = population;
        i.name = name;


        string fileName = null;
        for (int j = 1; j < 21; j++)
        {
            fileName = "pop" + j + ".bin";
            if (!File.Exists(fileName))
                break;
            else if (j == 20)
                File.Delete(fileName);
        }
        

        FileStream stream = File.Create(fileName);
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, i);
        stream.Close();
    }


    public IndividualToSave LoadIndividual(string name)
    {
        var formatter = new BinaryFormatter();
        FileStream stream = File.OpenRead(name);
        IndividualToSave i = (IndividualToSave)formatter.Deserialize(stream);
        stream.Close();
        return i;
    }

    public PopulationToSave LoadPopulation(string name)
    {
        var formatter = new BinaryFormatter();
        FileStream stream = File.OpenRead(name);
        PopulationToSave i = (PopulationToSave)formatter.Deserialize(stream);
        stream.Close();
        return i;
    }

    public void SaveCSV(List<Individual> pop, int generation)
    {
        try
        {
            if (File.Exists("generation.csv") && generation == 1)
            {
                File.Delete("generation.csv");
            }

            double lowestFitness = double.MaxValue;
            double highestFitness = 0.0d;
            double average = 0;

            foreach (Individual i in pop)
            {
                average += i.fitness;
                if (i.fitness > highestFitness)
                    highestFitness = i.fitness;
                if (i.fitness < lowestFitness)
                    lowestFitness = i.fitness;
            }

            average = average / pop.Count;


            using (System.IO.StreamWriter file = new StreamWriter("generation.csv", true))
            {
                if (generation == 1)
                    file.WriteLine("Average,Highest Fitness,Lowest Fitness");

                file.WriteLine(average + "," + highestFitness + "," + lowestFitness);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Couldn't save to csv: " + ex);
        }
    }

    public void SaveHighscore(List<HighScoreData> listForHighScore)
    {
        ListHighScoreData list = new ListHighScoreData();
        if (File.Exists("highscores.bin"))
        {
            var formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead("highscores.bin");
            list = (ListHighScoreData)formatter.Deserialize(stream);
            stream.Close();
        }


        foreach (HighScoreData data in listForHighScore)
        {
            list.list.Add(data);
        }

        File.Delete("highscores.bin");

        FileStream stream2 = File.Create("highscores.bin");
        var formatter2 = new BinaryFormatter();
        formatter2.Serialize(stream2, list);
        stream2.Close();
    }

    public ListHighScoreData LoadHighscore()
    {
        ListHighScoreData list = new ListHighScoreData();

        if (File.Exists("highscores.bin"))
        {
            var formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead("highscores.bin");
            list = (ListHighScoreData)formatter.Deserialize(stream);
            stream.Close();
        }

        return list;
    }
}
