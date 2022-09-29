using Controls;
using Unity.Netcode;
using UnityEngine;

namespace Car
{
    public class CarPrefabManager : NetworkBehaviour
    {
        [SerializeField] private Transform carAnchor;
        [SerializeField] private CarInputHandler carInputHandler;
        [SerializeField] private CarPrefabProvider carPrefabProvider;

        private CarPrefabTerm carPrefabTerm;

        public override void OnNetworkSpawn()
        {
            carPrefabTerm = CarSelection.CarSelected;
        }
        
        private void Start()
        {
            SetupCarPrefab(carPrefabTerm);
        }

        private void SetupCarPrefab(CarPrefabTerm carPrefabTerm)
        {
            GameObject prefab = carPrefabProvider.GetCarPrefab(carPrefabTerm);
            GameObject newCarChassis = Instantiate(prefab, carAnchor);
            carInputHandler.CarWheelAnimations = newCarChassis.GetComponent<CarWheelAnimations>();
        }
    }
}
