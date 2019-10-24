using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        // Variables
        Transform target;
        Mover mover;
        [SerializeField] float weaponRange = 1f;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (!target) return;
            MoveWithinRange();
        }

        // Moves within range of weapon and stops
        private void MoveWithinRange()
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) <= weaponRange;
            if (!isInRange)
            {
                mover.MoveToTarget(target.position);
            }
            else
            {
                mover.MoveStop();
                Nulltarget();
            }
        }

        // Nulls the target variable
        public void Nulltarget()
        {
            target = null;
        }

        // Sets the target to be attacked
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(GetComponent<Fighter>());
            target = combatTarget.transform;
        }
    }
}
