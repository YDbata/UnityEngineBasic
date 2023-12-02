using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName;//ó���� bool�� ���� �̸�
    public bool updateOnState;//State���� ó���� ���� ����
    public bool updateOnStateMachine;//StateMachine���� ó���� ���� ����
    public bool valueOnEnter, valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
