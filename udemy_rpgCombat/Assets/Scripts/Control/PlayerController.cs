using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Cache
        Fighter fighter;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nothing Here");
        }

        // Returns a ray drawn between the main camera and the cursor position
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Draws a ray from camera to mouse position, records the hit location in world space and passes it to the Mover script
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;
            if (Input.GetMouseButton(0))
            {
                GetComponent<Mover>().MoveAction(hit.point);
            }
            return true;
        }

        // Draws a ray and calls the Fighter.Attack method if it finds a valid CombatTarget
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (Input.GetMouseButton(0))
                {
                    fighter.SetTarget(target);
                }
                return true;
            }
            return false;
        }
    }
}
