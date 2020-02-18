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
        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float chaseDistance = 5f;

        // Cache
        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        // Called on Start
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindGameObjectWithTag("Player");
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
            if (player == null) return;
            bool canSeePlayer = Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
            if (canSeePlayer)
            {
                timeSinceLastSawPlayer = 0f;
                AttackTarget(player);
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                GetComponent<IAction>().Cancel();
            }
            else
            {
                mover.MoveAction(guardPosition);
            }
            timeSinceLastSawPlayer += Time.deltaTime;
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

