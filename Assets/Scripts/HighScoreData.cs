using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class HighScoreData
{
    public string name { get; set; }
    public float time { get; set; }
    public int map { get; set; }
    public HighScoreData(string name, float time, int map) { this.name = name; this.time = time; this.map = map; }
}

[System.Serializable]
public class ListHighScoreData
{
    public List<HighScoreData> list = new List<HighScoreData>();
}