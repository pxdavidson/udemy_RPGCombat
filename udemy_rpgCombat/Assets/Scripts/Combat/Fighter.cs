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
        Health target;
        Mover mover;
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float weaponDamage = 5;
        [SerializeField] float timeBetweenAttacks = 1f;
        float timeSinceLastAttack;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (ValidTarget())
            {
                MoveWithinRange();
            }
        }

        public bool ValidTarget()
        {
            if (!target || target.ReturnAliveState() == false)
            {
                GetComponent<Animator>().SetTrigger("stopattacking");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Cancel()
        {
            NullTarget();
        }

        // Sets the target to be attacked
        public void SetTarget(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(GetComponent<Fighter>());
            target = combatTarget.GetComponent<Health>();
        }

        // Moves within range of weapon and stops
        private void MoveWithinRange()
        {
            bool isInRange = Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
            if (!isInRange)
            {
                mover.MoveToTarget(target.transform.position);
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
            transform.LookAt(target.transform);
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
            target.SetHealth(weaponDamage);
        }
    }
}
