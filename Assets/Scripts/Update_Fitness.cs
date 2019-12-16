using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_Fitness : MonoBehaviour
{

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CarAI car = transform.parent.parent.GetComponent<CarAI>();
        if (Parameters.display)
            text.text = (car.fitness * (car.checkpoints / 4)).ToString() + " : " + car.checkpoints.ToString();
        else
            text.text = "";
    }
}
