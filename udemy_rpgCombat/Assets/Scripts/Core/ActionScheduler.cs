using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        // Variables
        MonoBehaviour currentAction;
        
        public void StartAction (MonoBehaviour action)
        {
            if (currentAction != null && action != currentAction)
            {
                print("cancel " + currentAction);
            }
            currentAction = action;
        }
    }
}