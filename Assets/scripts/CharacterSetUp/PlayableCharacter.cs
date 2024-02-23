using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CharacterType
{
    Cat,
    Hound
}
public class PlayableCharacter : MonoBehaviour
{
    [Header("Character Health")]
    [SerializeField] private float hP = 100f;

    [Header("Animation Info")]
    private Animator _animator;
    private enum States { Idle, Run, Attack };
    private States _state;


    [Header("Movement Info")]
    [SerializeField] float speed = 50f;
    [HideInInspector] public Vector3 Target;
    [HideInInspector] public float Offset;

    [Header("Attack Info")]
    public float attackRange = 2f;

    [HideInInspector] public Transform enemyTarget;

    [SerializeField] private Transform attackPoint;


    [HideInInspector]
    public CharacterManager manager;

    [SerializeField] private LayerMask characterMask;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private LayerMask EnemyMask;

    [SerializeField]
    CharacterType characterType;

    [SerializeField]
    EnemyType enemySpeciality;

    [SerializeField] private ParticleSystem dust;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _state = States.Idle;
        Target = transform.position;
        enemyTarget = null;
        Offset = .01f;
        attackPoint = gameObject.transform;
    }

   
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Target) <= Offset)
        {
            dust.Stop();
            var res = FindEnemy();
            if (res.Length > 0)
            {
                _state = States.Attack;
                Attack();
            }
            else
                _state = States.Idle;

            _animator.SetInteger("State", 0);
        }
        else
        {
            Movement(Target);
            _state = States.Run;
            _animator.SetInteger("State", 1);
            dust.Play();
        }
        
    }
    
    internal void ChangeTarget(Vector3 target, float offset)
    {
        Target = target;
        Offset = offset;
    }

    internal Collider2D[] FindEnemy()
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyMask);
    }

    private void Movement(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Direction(target);
    }

    protected virtual void Attack() 
    {
        var enemies = FindEnemy();
        var shortestdistance = Mathf.Infinity;
        Collider2D shortestenemy = null;
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < shortestdistance)
                shortestenemy = enemy;
            enemy.TryGetComponent(out Enemy enemy1);

            if (enemy1.EnemType == enemySpeciality)
            {
                if (!enemy1.Defeated)
                {
                    enemy1.TakeDamage(100);
                    _animator.SetTrigger("Attack");
                }
            }
        }

        if (enemyTarget != null && Vector3.Distance(transform.position, enemyTarget.position) <= attackRange)
            Direction(enemyTarget.position);
        else
        {
            if (shortestenemy == null)
                return;
            shortestenemy.TryGetComponent(out Enemy enemy1);
            if (enemy1.EnemType == enemySpeciality)
            {
                if (!enemy1.Defeated)
                    Direction(shortestenemy.transform.position);
            }
            enemyTarget = null;
        }
    }
    internal void Direction(Vector3 target)
    {
        if (target.x > transform.position.x)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        else if (target.x < transform.position.x)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
