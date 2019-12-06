using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // Variables
        [SerializeField] float chaseDistance = 5f;

        // Update is called once per frame
        void Update()
        {
            LookForPlayer();
        }

        private void LookForPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
            bool canSeePlayer = Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
            if (canSeePlayer)
            {
                print("chase " +this.name +player.name);
            }
            else
            {
                return;
            }
        }
    }
}

