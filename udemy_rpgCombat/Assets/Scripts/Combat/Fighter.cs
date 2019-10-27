using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // Variables
        Transform target;
        Mover mover;
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float timeBetweenAttacks = 1f;
        float timeSinceLastAttack;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (!target) return;
            timeSinceLastAttack += Time.deltaTime;
            MoveWithinRange();
        }

        public void Cancel()
        {
            NullTarget();
        }

        // Sets the target to be attacked
        public void SetTarget(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(GetComponent<Fighter>());
            target = combatTarget.transform;
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
                if (timeSinceLastAttack >= timeBetweenAttacks)
                {
                    AttackTarget();
                }
            }
        }

        private void AttackTarget()
        {
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0f;
        }

        // Nulls the target variable
        public void NullTarget()
        {
            target = null;
        }

        // Animation event
        void Hit()
        {
            Debug.Log("Pow!");
        }
    }
}
