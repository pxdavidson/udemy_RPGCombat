using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        // Variables
        [SerializeField] float health;
        bool isAlive = true;

        // Cache
        ActionScheduler actionscheduler;

        // Called on Start
        private void Start()
        {
            actionscheduler = GetComponent<ActionScheduler>();
        }

        public void SetHealth(float damage)
        {
            if (!isAlive)
            {
                return;
            }
            else
            {
                health = Mathf.Max(health - damage, 0);
            }
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isAlive = false;
            actionscheduler.StopActions();
        }

        public bool IsAlive()
        {
            return isAlive;
        }
    }
}
