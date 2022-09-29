using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    [CreateAssetMenu(fileName = "CarPrefabProvider", menuName = "ScriptableObjects/CarPrefabProvider")]
    public class CarPrefabProvider : ScriptableObject
    {
        [SerializeField] private List<CarPrefab> carPrefabs;

        public List<CarPrefab> CarPrefabs => carPrefabs;

        private Dictionary<CarPrefabTerm, GameObject> carPrefabsDictionary = new();
        
        public GameObject GetCarPrefab(CarPrefabTerm carTerm)
        {
            if (carPrefabsDictionary.Count <= 0)
            {
                InitializeDictionary();
            }

            return carPrefabsDictionary[carTerm];
        }

        private void InitializeDictionary()
        {
            carPrefabsDictionary = new Dictionary<CarPrefabTerm, GameObject>();
            foreach (CarPrefab carPrefab in carPrefabs)
            {
                carPrefabsDictionary.Add(carPrefab.Term, carPrefab.Car);
            }
        }
    }

    [Serializable]
    public class CarPrefab
    {
        [SerializeField] private string title;
        [SerializeField] private Color color;
        [SerializeField] private CarPrefabTerm term;
        [SerializeField] private GameObject car;

        public Color Color => color;
        public CarPrefabTerm Term => term;
        public GameObject Car => car;
    }
}
