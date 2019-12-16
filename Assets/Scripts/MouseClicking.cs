using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClicking : MonoBehaviour
{
    public Sprite yellow;
    public Sprite blue;
    public Sprite green;
    public Sprite purple;
    public Sprite red;
    public Button button;
    public GameLoop gm;
    
    private GameObject previousCar = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1 << LayerMask.NameToLayer("car"));
            bool deselect = true;
            if (hit.collider != null && hit.transform.gameObject.tag != "wall" && hit.transform.gameObject.tag != "ui")
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = yellow;
                if (previousCar != null)
                    previousCar.GetComponent<SpriteRenderer>().sprite = red;
                previousCar = hit.transform.gameObject;
                deselect = false;
                button.GetComponentInChildren<Text>().text = "Save Current Car";
                gm.updateIndividual(hit.transform.gameObject.GetComponent<CarAI>().individual);
            }

            if (hit.collider != null && hit.transform.gameObject.tag == "ui")
            {
                deselect = false;
            }

            if (deselect)
            {
                if (previousCar != null)
                    previousCar.GetComponent<SpriteRenderer>().sprite = red;
                previousCar = null;
                button.GetComponentInChildren<Text>().text = "Save Current Population";
            }
        }
    }

    public void OnClick()
    {
        
    }
}
