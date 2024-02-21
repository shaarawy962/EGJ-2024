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
    public float speed = 50f;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float offset;
    
    [Header("Attack Info")]
    public float attackRange = 2f;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private Transform attackPoint;
    
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _state = States.Idle;
        target = transform.position;
        offset = .01f;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, target) < offset)
        {
            if (FindEnemy().Length > 0)
                _state = States.Attack;
            else
                _state = States.Idle;
        }
        else 
            _state = States.Run;
        _animator.SetInteger("state" , (int) _state);
    }
    internal Collider2D[] FindEnemy()
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyMask);
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
