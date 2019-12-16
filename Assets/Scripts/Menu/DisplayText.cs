using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{

    public int value;
    public InputField input;

    public void Start()
    {
        switch (value)
        {
            case 0:
                input.text = Parameters.mutationRate.ToString();
                break;
            case 1:
                input.text = Parameters.crossoverRate.ToString();
                break;
            case 2:
                input.text = Parameters.populationSize.ToString();
                break;
            case 3:
                input.text = Parameters.hiddenNodes.ToString();
                break;
            case 4:
                input.text = Parameters.hiddenLayers.ToString();
                break;
        }

    }
}
