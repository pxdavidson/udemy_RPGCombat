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
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float weaponDamage = 5;
        [SerializeField] float timeBetweenAttacks = 1f;
        float timeSinceLastAttack;

        // Cache
        Mover mover;
        Animator animator;

        // Called at Start
        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        // Updated every frame
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (ValidTarget())
            {
                MoveWithinRange();
            }
        }

        // Checks if the target is null, and if not null, is it "alive". Returns true if neither condition is met
        public bool ValidTarget()
        {
            if (!target || target.IsAlive() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // IAction method called to clear the target variable and reset animation
        public void Cancel()
        {
            NullTarget();
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopattacking");
        }

        // Sets the ActionScheduler for combat then sets the global target variable
        public void SetTarget(GameObject targetGameObject)
        {
            GetComponent<ActionScheduler>().StartAction(GetComponent<Fighter>());
            target = targetGameObject.GetComponent<Health>();
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
            animator.ResetTrigger("stopattacking");
            animator.SetTrigger("attack");
            timeSinceLastAttack = 0f;
        }

        // Nulls the target variable
        public void NullTarget()
        {
            target = null;
        }

        // Animation event
        public void Hit()
        {
            if (target == null) return;
            target.SetHealth(weaponDamage);
        }
    }
}
