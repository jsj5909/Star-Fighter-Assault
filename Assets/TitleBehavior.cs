using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBehavior : StateMachineBehaviour
{


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //-282, 173.5

        RectTransform rect = animator.gameObject.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,161);

      // rect.anchoredPosition = new Vector3(-282, 173.5f, 0);

      //  rect.position = new Vector3(4.0855f, 161);

      //  Vector3 pos = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,)

      //  animator.gameObject.GetComponent<RectTransform>().position = pos;
    }

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
