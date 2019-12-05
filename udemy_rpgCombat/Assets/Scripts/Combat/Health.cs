using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        // Variables
        [SerializeField] float health;
        bool isAlive = true;

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
        }

        public bool ReturnAliveState()
        {
            return isAlive;
        }
    }
}
