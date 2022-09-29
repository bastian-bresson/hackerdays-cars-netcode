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
            image.sprite = carPrefab.Sprite;
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
                currentlySelected.sprite = carPrefab.Sprite;
            }
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
