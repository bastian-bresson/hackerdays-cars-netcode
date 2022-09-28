using Unity.Netcode;
using UnityEngine;

namespace Controls
{
    public class CarInputHandler : NetworkBehaviour
    {
        [SerializeField] private CarMovement carMovement;
        [SerializeField] private CarWheelAnimations carWheelAnimations;
        
        // Update is called once per frame
        void Update()
        {
            if (!IsOwner)
            {
                return;
            }
            
            Vector3 inputVector = Vector3.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            carMovement.SetInputVectorServerRpc(inputVector);

            UpdateWheelSpin(inputVector.y);
        }

        private void UpdateWheelSpin(float inputY)
        {
            switch (inputY)
            {
                case > 0:
                    carWheelAnimations.MoveWheelsForwards();
                    break;
                case < 0:
                    carWheelAnimations.MoveWheelsBackwards();
                    break;
                default:
                    carWheelAnimations.StopMovingWheels();
                    break;
            }
        }
    }
}
