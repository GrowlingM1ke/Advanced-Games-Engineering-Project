using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intial_Value_Toggle : MonoBehaviour
{
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = Parameters.load;
    }

    // Update is called once per frame
    void Update()
    {
       Parameters.load = toggle.isOn;
    }
}
