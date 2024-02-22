using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // Stats
    public float maxHealth;
    public float movementSpeed;
    public float threshold;
    public float runAwayMultiplier;

    private bool defeated = false;
    private bool passedThreshold = false;
    private float health;
    private FloatingHealthBar healthBar;

    public bool Defeated => defeated;
    [SerializeField] GameObject target;

    private void Awake()
    {
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging threshold line
        Debug.DrawLine(new Vector2(threshold, -1000), new Vector2(threshold, 1000), Color.red, 2.5f);

        if (health <= 0 && defeated == false)
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
        health = 0;
        defeated = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 1.0f);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
            Flee();
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
