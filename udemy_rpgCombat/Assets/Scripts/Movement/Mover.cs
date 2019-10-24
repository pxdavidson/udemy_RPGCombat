using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        // Recieves a Vector 3 and sets the NavMesh Destination
        public void MoveToTarget(Vector3 targetDestination)
        {
            GetComponent<NavMeshAgent>().SetDestination(targetDestination);
        }

        // Calculates the velocity from the NavMeshAgent and sends it to the animation blend tree
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}
