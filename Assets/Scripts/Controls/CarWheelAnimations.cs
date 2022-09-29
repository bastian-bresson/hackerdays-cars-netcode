using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class CarWheelAnimations : MonoBehaviour
    {
        [Header("Front Wheels")] 
        [SerializeField] private Transform leftFrontWheel;
        [SerializeField] private Transform rightFrontWheel;
        
        [Header("Wheel Animators")]
        [SerializeField] private List<Animator> wheels;

        [Header("Wheel Settings")] 
        [SerializeField] private float turnSpeed = 2f;
        [SerializeField] private float maxTurnAngle = 20f;

        public void TurnWheels(float inputVectorX)
        {
            TurnWheel(inputVectorX, leftFrontWheel);
            TurnWheel(inputVectorX, rightFrontWheel);
        }

        private void TurnWheel(float inputVectorX, Transform wheel)
        {
            if(inputVectorX < 0 && wheel.localRotation.y > -maxTurnAngle)
                wheel.localRotation = Quaternion.Slerp(wheel.localRotation, Quaternion.Euler(0,-maxTurnAngle,0), Time.deltaTime * 2f);
            else if(inputVectorX > 0 && wheel.localRotation.y < maxTurnAngle)
                wheel.localRotation = Quaternion.Slerp(wheel.localRotation, Quaternion.Euler(0,maxTurnAngle,0), Time.deltaTime * 2f);
                
            if(inputVectorX == 0)
                wheel.localRotation = Quaternion.Slerp(wheel.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime * turnSpeed);
        }
        
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
