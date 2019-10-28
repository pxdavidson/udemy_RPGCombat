using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        // Variables
        [SerializeField] float health;

        public void SetHealth(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
        }
    }
}
