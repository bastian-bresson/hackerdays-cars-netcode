using System;
using UnityEngine;

namespace Car
{
    public class CarPrefabManager : MonoBehaviour
    {
        [SerializeField] private Transform carAnchor;
        [SerializeField] private CarPrefabProvider carPrefabProvider;

        private void Awake()
        {
            SetupCarPrefab(CarPrefabTerm.Race);
        }

        public void SetupCarPrefab(CarPrefabTerm carPrefabTerm)
        {
            GameObject prefab = carPrefabProvider.GetCarPrefab(carPrefabTerm);
            Instantiate(prefab, carAnchor);
        }
    }
}
