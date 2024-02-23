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
    public float knockbackForce;

    private bool defeated = false;
    private bool passedThreshold = false;
    [SerializeField]private float health;
    private FloatingHealthBar healthBar;
    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging threshold line
#if UNITY_EDITOR
        Debug.DrawLine(new(threshold, -1000), new(threshold, 1000), Color.red, 2.5f);
#endif

        if (health <= 0 && defeated == false)
        {
            defeated = true;
            GetComponent<SpriteRenderer>().flipX = true;
            movementSpeed *= runAwayMultiplier;

            //shady's code
            GotDefeated?.Invoke(this);
        }

        if (transform.position.x <= threshold)
        {
            passedThreshold = true;
        }

        Vector3 newTarget;
        if (passedThreshold)
        {
            newTarget = target.transform.position;
        }
        else
        {
            // Do not modify "y" component
            newTarget = new(target.transform.position.x, transform.position.y, target.transform.position.z);
        }

        Vector3 direction = newTarget - transform.position;
        direction = direction.normalized;
        direction = defeated ? -direction : direction;

        float step = movementSpeed * Time.deltaTime;
        transform.position += direction * step;
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

        Vector3 knockback = Vector3.right * knockbackForce;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
