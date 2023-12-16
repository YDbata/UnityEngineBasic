using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    IAction currentAction;
    public void StartAction(IAction action)
    {
        if (currentAction == action) return;
        if(currentAction != null)
        {
            currentAction.Cancle();
        }
        currentAction = action;
    }

    public void CancleCurrentAction()
    {
        StartAction(null);
    }
}
