using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class CarWheelAnimations : MonoBehaviour
    {
        [SerializeField] private List<Animator> wheels;

        public void MoveWheelsBackwards()
        {
            // TODO
        }
        
        public void MoveWheelsForwards()
        {
            foreach (Animator animator in wheels)
            {
                animator.enabled = true;
            }
        }

        public void StopMovingWheels()
        {
            foreach (Animator animator in wheels)
            {
                animator.enabled = false;
            }
        }
    }
}
