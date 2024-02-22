using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetUp : MonoBehaviour
{
   [Header("Character Health")]
    [SerializeField] private float hP = 100f;
  
   [Header("Animation Info")]
    private Animator _animator;
    private enum States { Idle, Run , Attack };
    private States _state;
   
    
    [Header("Movement Info")]
    [SerializeField] float speed = 50f;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float offset;
    
    [Header("Attack Info")]
    public float attackRange = 2f;

    [HideInInspector] public Transform enemyTarget;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private Transform attackPoint;
    
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _state = States.Idle;
        target = transform.position;
        enemyTarget = null;
        offset = .01f;
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target) <= offset)
        {
            if (FindEnemy().Length > 0)
            {
                _state = States.Attack;
               Attack();
            }
            else
                _state = States.Idle;
        }
        else
        {
            Movement(target);
            _state = States.Run;
        }
        _animator.SetInteger("state" , (int) _state);
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

    private void Attack()
    {
        var enemies = FindEnemy();
        var shortestdistance = Mathf.Infinity;
        Collider2D shortestenemy = null;
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < shortestdistance)
                shortestenemy = enemy;
        }
        
        if (enemyTarget != null && Vector3.Distance(transform.position, enemyTarget.position) <= attackRange)
            Direction(enemyTarget.position);
        else
        {
            if (shortestenemy == null) 
                return;
            Direction(shortestenemy.transform.position);
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
        Gizmos.DrawWireSphere(attackPoint.position , attackRange);
    }
}
