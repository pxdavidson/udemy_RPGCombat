using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        // Variables
        IAction currentAction;
        
        public void StartAction (IAction action)
        {
            if (currentAction != null && action != currentAction)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
    }
}