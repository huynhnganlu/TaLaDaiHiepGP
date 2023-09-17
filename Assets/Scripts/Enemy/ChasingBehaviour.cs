using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyController>().LookAtPlayer();

        if(Vector2.Distance(MyCharacterController.Instance.transform.position, animator.transform.position) < attackRange)
        {
            animator.SetTrigger("Attack");

           
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, MyCharacterController.Instance.transform.position, speed * Time.deltaTime) ;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    
}
