using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : StateMachineBehaviour
{
    private Transform _character;
    private CharacterSetUp _setUp;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _character = animator.gameObject.transform;
        _setUp = animator.gameObject.GetComponent<CharacterSetUp>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Movement(_setUp.target);
    }
    private void Movement(Vector3 target)
    {
       // _character.position = Vector3.MoveTowards(_character.position, target, _setUp.speed * Time.deltaTime);
        _setUp.Direction(target);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
