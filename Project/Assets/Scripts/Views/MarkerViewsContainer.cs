using UnityEngine;

namespace Views
{
    public class MarkerViewsContainer : MonoBehaviour
    {
        [field: SerializeField] public MarkerView[] MarkerViews { get; private set; }

        private void Awake()
        {
            foreach (var markerView in MarkerViews)
            {
                markerView.SetActive(true);
            }
        }
    }
}