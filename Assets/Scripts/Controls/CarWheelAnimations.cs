using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class CarWheelAnimations : MonoBehaviour
    {
        [SerializeField] private List<Animator> wheels;

        public void SpinWheels()
        {
            foreach (Animator animator in wheels)
            {
                animator.enabled = true;
            }
        }

        public void StopWheels()
        {
            foreach (Animator animator in wheels)
            {
                animator.enabled = false;
            }
        }
    }
}
