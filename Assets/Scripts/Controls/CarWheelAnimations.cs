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
            foreach (Animator animator in wheels)
            {
                animator.enabled = true;
                animator.Play("Backwards");
            }
        }
        
        public void MoveWheelsForwards()
        {
            foreach (Animator animator in wheels)
            {
                animator.enabled = true;
                animator.Play("Forwards");
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
