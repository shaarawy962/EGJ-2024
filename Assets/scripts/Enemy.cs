using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    Fox,
    Snake
}

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


    //shady's code
    public Action<Enemy> GotDefeated;

    [SerializeField]
    EnemyType type;

    public EnemyType EnemType => type;


    private void Awake()
    {
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging threshold line
#if UNITY_EDITOR
        Debug.DrawLine(new Vector2(threshold, -1000), new Vector2(threshold, 1000), Color.red, 2.5f);
#endif

        if (health <= 0 && defeated == false)
        {
            defeated = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            movementSpeed *= runAwayMultiplier;

            //shady's code
            GotDefeated?.Invoke(this);
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
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 1.0f);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar?.UpdateHealthBar(health, maxHealth);
    }
}
