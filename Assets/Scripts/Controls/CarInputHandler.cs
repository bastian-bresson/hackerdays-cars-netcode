using UnityEngine;

namespace Controls
{
    public class CarInputHandler : MonoBehaviour
    {
        [SerializeField] private CarWheelAnimations carWheelAnimations;
        [SerializeField] private CarMovement carMovement;

        // Update is called once per frame
        void Update()
        {
            Vector3 inputVector = Vector3.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            carMovement.SetInputVector(inputVector);

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
