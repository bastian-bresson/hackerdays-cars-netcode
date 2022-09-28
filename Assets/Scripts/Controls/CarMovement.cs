using Unity.Netcode;
using UnityEngine;

namespace Controls
{
    public class CarMovement : NetworkBehaviour
    {
        [Header("Car Components")]
        [SerializeField] private Rigidbody carRigidBody;
        [SerializeField] private CarWheelAnimations carWheelAnimations;

        [Header("Car Movement Settings")]
        [SerializeField] private float driftFactor = 0.95f;
        [SerializeField] private float accelerationFactor = 30f;
        [SerializeField] private float turnFactor = 3.5f;

        private float accelerationInput = 0;
        private float steeringInput = 0;

        private float rotationAngle = 0;

        // Update is called once per frame
        void FixedUpdate()
        {
            ApplyEngineForce();
        
            //KillOrthogonalVelocity();
        
            ApplySteering();
        }

        private void ApplyEngineForce()
        {
            Vector3 engineForceVector = transform.forward * accelerationInput * accelerationFactor;

            carRigidBody.AddForce(engineForceVector, ForceMode.Force);
        }

        private void ApplySteering()
        {
            rotationAngle -= steeringInput * turnFactor;
            carRigidBody.MoveRotation(Quaternion.Euler(0,rotationAngle,0));
        }

        //HELP. How to vector math when not 2D?!?
        private void KillOrthogonalVelocity()
        {
            Vector3 forwardVelocity = transform.forward * Vector3.Dot(carRigidBody.velocity, transform.forward);
            Vector3 rightVelocity = transform.right * Vector3.Dot(carRigidBody.velocity, transform.right);

            //carRigidBody.velocity = forwardVelocity * rightVelocity * driftFactor;
        }
    
        public void SetInputVector(Vector3 inputVector)
        {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }
    
    }
}
