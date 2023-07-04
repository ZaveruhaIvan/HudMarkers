using UnityEngine;

namespace Updaters
{
    public class ScaleDistanceBasedUpdater : MonoBehaviour
    {
        [SerializeField, Header("Refs")] private Transform _firstObject;
        [SerializeField] private Transform _secondObject;
        [SerializeField] private Transform _scalableObject;
        [Space]
        [SerializeField, Header("Settings")] private Vector3 _minScale = Vector3.one;
        [SerializeField] private Vector3 _maxScale = Vector3.one;
        [SerializeField, Range(0.01f, 1000f)] private float _minDistance = 1f;
        [SerializeField, Range(0.01f, 1000f)] private float _maxDistance = 10f;

        private void Start()
        {
            SetScale();
        }

        private void Update()
        {
            SetScale();
        }

        private void SetScale()
        {
            var distance = Vector3.Distance(_firstObject.position, _secondObject.position);
            var clampedDistance = Mathf.Clamp(distance, _minDistance, _maxDistance);
            var normalizedDistance = 1 - (clampedDistance - _minDistance) / (_maxDistance - _minDistance);
            var scale = normalizedDistance * (_maxScale - _minScale) + _minScale;
            _scalableObject.localScale = scale;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_minDistance > _maxDistance)
                _minDistance = _maxDistance;
        }
#endif
    }
}