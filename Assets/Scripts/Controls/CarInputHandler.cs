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
            
            if(inputVector != Vector3.zero)
                carWheelAnimations.SpinWheels();
            else
                carWheelAnimations.StopWheels();
        }
    }
}
