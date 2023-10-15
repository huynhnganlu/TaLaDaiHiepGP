using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectPoolController.Instance.ReturnObjectToPool(animator.gameObject);
    }


}
