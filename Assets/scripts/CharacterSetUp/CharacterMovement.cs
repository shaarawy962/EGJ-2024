using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement: MonoBehaviour
{
    [SerializeField] private float speed;
    internal Vector3 Target { get;set; }
    private CharacterAnimation _animation;
    private void Start()
    {
        Target = transform.position;
        _animation = GetComponent<CharacterAnimation>();
    }
    private void FixedUpdate()
    {
        Movement(Target);
    }
    private void Movement(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) < .01f)
        {
            _animation.State = CharacterAnimation.States.Idle;
            return;
        }
        _animation.State = CharacterAnimation.States.Run;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Direction(target);
    }
    private void Direction(Vector3 target)
    {
        if (target.x >= transform.position.x)
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        else 
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
    }
}
