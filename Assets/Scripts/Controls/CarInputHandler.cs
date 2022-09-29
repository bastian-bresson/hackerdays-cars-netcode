using Unity.Netcode;
using UnityEngine;

namespace Controls
{
    public class CarInputHandler : NetworkBehaviour
    {
        [SerializeField] private CarMovement carMovement;
        
        public CarWheelAnimations CarWheelAnimations { get; set; }
        
        private void Update()
        {
            if (!IsOwner)
                return;

            Vector3 inputVector = Vector3.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            carMovement.SetInputVectorServerRpc(inputVector);

            UpdateWheelSpin(inputVector.y);
            CarWheelAnimations.TurnWheels(inputVector.x);
        }

        private void UpdateWheelSpin(float inputVectorY)
        {
            switch (inputVectorY)
            {
                case > 0:
                    CarWheelAnimations.MoveWheelsForwards();
                    break;
                case < 0:
                    CarWheelAnimations.MoveWheelsBackwards();
                    break;
                default:
                    CarWheelAnimations.StopMovingWheels();
                    break;
            }
        }
    }
}
