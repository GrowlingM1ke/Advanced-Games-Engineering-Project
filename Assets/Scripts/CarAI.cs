using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Individual individual;
    Collider2D col;
    public float fitness;
    Vector3 lastposition;
    public int id;
    public bool isDone = false;
    float speedForce = 15.0f;
    float torqueForce = -200.0f;
    float driftFactor = 0.75f;
    float time = 0.0f;
    public float checkpoints = 0;
    public bool playerControl = false;
    bool hitWall;
    public GameObject me;
    public Sprite red;
    public Sprite green;
    public Sprite purple;
    public Sprite blue;
    public Sprite yellow;
    private bool finished = false;
    public float finalTime = 0.0f;
    public Text text;
    public string name;

    void Start()
    {
        // wall is 9
        col = GetComponent<BoxCollider2D>();
        fitness = 0;
        lastposition = transform.position;
        hitWall = false;
        finished = false;
        if (Parameters.race && playerControl)
        {
            if (Parameters.colour == "Red")
                GetComponent<SpriteRenderer>().sprite = red;
            if (Parameters.colour == "Green")
                GetComponent<SpriteRenderer>().sprite = green;
            if (Parameters.colour == "Purple")
                GetComponent<SpriteRenderer>().sprite = purple;
            if (Parameters.colour == "Blue")
                GetComponent<SpriteRenderer>().sprite = blue;
            if (Parameters.colour == "Yellow")
                GetComponent<SpriteRenderer>().sprite = yellow;
        }
        if (Parameters.race)
        {
            time = -3.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time < 0.0f)
        {
            time += Time.deltaTime;
        }

        else if (playerControl)
        {
            time += Time.deltaTime;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (Input.GetButton("Accelerate"))
            {
                rb.AddForce(transform.right * speedForce);
            }

            if (Input.GetKey("left") || Input.GetKey("right"))
                CustomInput.SetAxis("Horizontal", Input.GetAxis("Horizontal"));
            else
                CustomInput.SetAxis("Horizontal", 0);


            rb.angularVelocity = (CustomInput.GetAxis("Horizontal") * torqueForce);

            rb.velocity = ForwardVelocity() + LeftVelocity() * driftFactor;

            if (hitWall && Parameters.wallDeath)
                Destroy(me);
        }
        else
        {
            // Update fitness
            time += Time.deltaTime;
            fitness += Vector3.Distance(lastposition, transform.position);
            lastposition = transform.position;
            // Check if collided with wall and if so update the fitness and destroy yourself
            if (hitWall || time > 70.0f || Parameters.killCommand)
            {
                isDone = true;
            }

            // Raycast
            double[] inputs = Raycast();


            // Now do movement based on that
            double[] outputs = individual.computeOutput(inputs);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (outputs[0] > 0.5)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * speedForce);
            }

            CustomInput.SetAxis("Horizontal", (float)outputs[1]);


            rb.angularVelocity = (CustomInput.GetAxis("Horizontal") * torqueForce);

            rb.velocity = ForwardVelocity() + LeftVelocity() * driftFactor;
        }

        if (Parameters.race && finished)
        {
            if (playerControl)
            {
                text.text = "Finished with time of " + time;
            }
            // Save highscore
            finished = false;
            if (name == null)
                Parameters.listForHighScore.Add(new HighScoreData("Player", finalTime, Parameters.track));
            else
                Parameters.listForHighScore.Add(new HighScoreData(name, finalTime, Parameters.track));

        }

        finalTime = time;
    }

    public void setIndividual(Individual individual, int id)
    {
        this.individual = individual;
        this.id = id;
    }

    public double[] Raycast()
    {
        float distanceForward = 10.0f;
        float distanceLeft = 10.0f;
        float distanceRight = 10.0f;
        Vector3 forward = transform.right;
        Vector3 left = (Quaternion.Euler(0, 0, -45) * transform.right);
        Vector3 right = (Quaternion.Euler(0, 0, 45) * transform.right);
        RaycastHit2D hitForward = Physics2D.Raycast(transform.position, forward, 10, 1 << LayerMask.NameToLayer("wall"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, left, 10, 1 << LayerMask.NameToLayer("wall"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, right, 10, 1 << LayerMask.NameToLayer("wall"));
        Debug.DrawRay(transform.position, forward * 10, Color.blue);
        Debug.DrawRay(transform.position, left * 10, Color.red);
        Debug.DrawRay(transform.position, right * 10, Color.yellow);
        if (hitForward.collider != null)
        {
            distanceForward = hitForward.distance;
        }
        if (hitRight.collider != null)
        {
            distanceRight = hitRight.distance;
        }
        if (hitLeft.collider != null)
        {
            distanceLeft = hitLeft.distance;
        }

        double[] inputs = new double[Parameters.inputs];
        inputs[0] = distanceForward;
        inputs[1] = distanceRight;
        inputs[2] = distanceLeft;

        return inputs;
    }

    Vector2 ForwardVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    Vector2 LeftVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "wall")
            hitWall = true;
        else if (col.gameObject.tag == "checkpoint")
        {
            Physics2D.IgnoreCollision(col.collider, this.col);
            if (checkpoints == col.transform.root.GetComponent<id>().value)
            {
                checkpoints = col.transform.root.GetComponent<id>().value + 1;
                if (checkpoints == 6 && Parameters.race)
                    finished = true;
            }
        }
    }
}

public static class CustomInput
{
    static Dictionary<string, float> inputs = new Dictionary<string, float>();

    static public float GetAxis(string _axis)
    {
        if (!inputs.ContainsKey(_axis))
        {
            inputs.Add(_axis, 0);
        }

        return inputs[_axis];
    }

    static public void SetAxis(string _axis, float _value)
    {
        if (!inputs.ContainsKey(_axis))
        {
            inputs.Add(_axis, 0);
        }

        inputs[_axis] = _value;
    }
}
