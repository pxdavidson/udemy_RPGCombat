using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        // Variable
        [SerializeField] Transform target;

        // Update is called once per frame
        void Update()
        {
            UpdatePosition();
        }

        // Sets the attached transforms position to match that of the target transforms position
        private void UpdatePosition()
        {
            transform.position = target.position;
        }
    }
}
