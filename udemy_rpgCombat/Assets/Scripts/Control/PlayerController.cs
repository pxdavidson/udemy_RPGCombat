using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            InteractWithMouse();
        }

        // Returns a ray drawn between the main camera and the cursor position
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void InteractWithMouse()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursorPos();
                InteractWithCombat();
            }
        }

        // Draws a ray from camera to mouse position, records the hit location in world space and passes it to the Mover script
        private void MoveToCursorPos()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveToTarget(hit.point);
            }
        }

        // Draws a ray and calls the Fighter.Attack method if it finds a valid CombatTarget
        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }
    }
}
