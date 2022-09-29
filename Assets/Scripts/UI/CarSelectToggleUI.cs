using Car;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CarSelectToggleUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Toggle toggle;
        [SerializeField] private CarPrefab carPrefab;

        private Image currentlySelected;
        
        public void SetupCarSelectToggle(ToggleGroup toggleGroup, Image currentlySelected, CarPrefab carPrefab)
        {
            image.color = carPrefab.Color;
            toggle.group = toggleGroup;
            this.currentlySelected = currentlySelected;
            this.carPrefab = carPrefab;

            toggle.onValueChanged.AddListener(SelectCar);
        }

        private void SelectCar(bool isToggled)
        {
            if (isToggled)
            {
                CarSelection.CarSelected = carPrefab.Term;
                currentlySelected.color = carPrefab.Color;
            }
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
