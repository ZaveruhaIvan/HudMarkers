using UnityEngine;

namespace Views
{
    public class MarkerView : MonoBehaviour
    {
        [SerializeField] private GameObject _targetPointer;
        [SerializeField] private RectTransform _marker;
        [SerializeField] private Transform _markerTargetPosition;

        public void SetActiveTargetPointer(bool isActive)
        {
            if (_targetPointer.activeSelf != isActive)
            {
                _targetPointer.SetActive(isActive);
            }
        }

        public void SetTargetPointerForwardLocalRotation(float angle)
        {
            _targetPointer.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void SetMarkerPosition(Vector2 markerPosition)
        {
            _marker.anchorMin = _marker.anchorMax = markerPosition;
        }

        public void SetActive(bool isActive)
        {
            if (gameObject.activeSelf != isActive)
            {
                gameObject.SetActive(isActive);
            }
        }
        
        public Vector3 MarkerTargetPosition => _markerTargetPosition.position;
    }
}