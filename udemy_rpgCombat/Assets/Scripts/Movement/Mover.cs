using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        // Cache
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void Cancel()
        {
            MoveStop();
        }

        public void MoveAction(Vector3 targetDestination)
        {
            GetComponent<ActionScheduler>().StartAction(GetComponent<Mover>());
            MoveToTarget(targetDestination);
        }

        // Recieves a Vector 3 and sets the NavMesh Destination
        public void MoveToTarget(Vector3 targetDestination)
        {
            navMeshAgent.SetDestination(targetDestination);
            navMeshAgent.isStopped = false;
        }

        // Stops the movement driven by the NavMeshAgent
        public void MoveStop()
        {
            navMeshAgent.isStopped = true;
        }

        // Calculates the velocity from the NavMeshAgent and sends it to the animation blend tree
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}
