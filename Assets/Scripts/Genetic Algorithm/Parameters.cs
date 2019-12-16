using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parameters
{
    public static int inputs = 3;
    public static int outputs = 2;
    public static int hiddenLayers = 2;
    public static int hiddenNodes = 4;
    public static double minValue = -3.0d;
    public static double maxValue = 3.0d;
    public static double mutationRate = 0.10d;
    public static double crossoverRate = 0.5d;
    public static int populationSize = 100;
    public static int track = 1;
    public static bool display = false;
    public static bool killCommand = false;
    public static bool load = false;
    public static string file = "";
    public static bool race = false;
    public static string[] carsToLoad = new string[3];
    public static string colour = "";
    public static bool collisions = false;
    public static bool wallDeath = false;
    public static List<HighScoreData> listForHighScore = new List<HighScoreData>();
}
