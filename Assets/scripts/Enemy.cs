using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Stats
    public int health;
    public float movementSpeed;
    public bool defeated;
    public float threshold;
    public float runAwayMultiplier;
    public bool passedThreshold = false;

    [SerializeField] GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // --------------------------------------
        // Debugging threshold line and Flee line
        float fleeThreshold = threshold - 3;
        Debug.DrawLine(new Vector2(threshold, -1000), new Vector2(threshold, 1000), Color.red, 2.5f);
        Debug.DrawLine(new Vector2(fleeThreshold, -1000), new Vector2(fleeThreshold, 1000), Color.yellow, 2.5f);

        // Testing defeated state
        Debug.Log(String.Format("{0}, defeated: {1}", name, defeated));
        Debug.Log(String.Format("health {0}", health));
        if (transform.position.x <= fleeThreshold)
        {
            health = 0;
        }
        // --------------------------------------

        if (health == 0 && defeated == false)
        {
            defeated = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            movementSpeed *= runAwayMultiplier;
        }

        if (transform.position.x <= threshold)
        {
            passedThreshold = true;
        }

        Vector2 newTarget;
        if (passedThreshold)
        {
            newTarget = target.transform.position;
        }
        else
        {
            // Do not modify "y" component
            newTarget = new(target.transform.position.x, transform.position.y);
        }

        Vector2 direction = (newTarget - (Vector2)transform.position);
        direction = direction.normalized;
        direction = defeated ? -direction : direction;

        float step = movementSpeed * Time.deltaTime;
        transform.position += (Vector3)direction * step;
    }

    public void Flee()
    {

    }
}
