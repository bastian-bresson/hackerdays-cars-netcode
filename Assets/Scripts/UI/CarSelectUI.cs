using Car;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CarSelectUI : MonoBehaviour
    {
        [SerializeField] private Image currentlySelected;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private Transform content;
        [SerializeField] private CarPrefabProvider carPrefabProvider;
        [SerializeField] private CarSelectToggleUI carSelectToggleUIPrefab;

        private void Awake()
        {
            foreach (CarPrefab carPrefab in carPrefabProvider.CarPrefabs)
            {
                CreateCarSelection(carPrefab);
            }
        }

        private void CreateCarSelection(CarPrefab carPrefab)
        {
            CarSelectToggleUI carSelectToggleUI = Instantiate(carSelectToggleUIPrefab, content);
            carSelectToggleUI.SetupCarSelectToggle(toggleGroup, currentlySelected, carPrefab);
        }
    }
}
