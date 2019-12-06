using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // Variables
        [SerializeField] float chaseDistance = 5f;
        Vector3 guardPosition;

        // Cache
        Fighter fighter;
        Health health;
        Mover mover;

        // Called on Start
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!health.IsAlive()) return;
            LookForPlayer();
        }

        // Checks for GameObjects with the "Player" tag then calls AttackTarget with the Vector3 position of the target.
        private void LookForPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
            bool canSeePlayer = Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
            if (canSeePlayer)
            {
                AttackTarget(player);
            }
            else
            {
                mover.MoveAction(guardPosition);
            }
        }

        // Passes the target to the Fighter script to process attacking
        private void AttackTarget(GameObject playerTarget)
        {
            fighter.SetTarget(playerTarget.gameObject);
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

