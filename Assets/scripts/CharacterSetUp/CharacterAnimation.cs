using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    internal enum States { Idle, Run };
    internal States State { get; set; }
    private Animator _animator;
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        State = States.Idle;
    }
    private void Update()
    {
        _animator.SetInteger("state" , (int) State);
    }
    
}
