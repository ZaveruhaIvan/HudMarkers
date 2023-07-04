using Helpers;
using Services;
using UnityEngine;
using Views;

namespace Updaters
{
    public class EllipseMarkersUpdater : MonoBehaviour
    {
        [SerializeField, Header("Refs")] private Camera _camera;

        [SerializeField, Range(0.01f, 1f), Header("Settings")] private float _ellipseCoefficientX = 0.5f;
        [SerializeField, Range(0.01f, 1f)] private float _ellipseCoefficientY = 0.5f;
        
        private MarkerViewsContainer _markerViewsContainer;

        private void Awake()
        {
            EllipseRadiusX = Constants.HalfViewport * _ellipseCoefficientX;
            EllipseRadiusY = Constants.HalfViewport * _ellipseCoefficientY;

            _markerViewsContainer = GameView.Instance.MarkerViewsContainer;
        }

        private void Update()
        {
            foreach (var markerView in _markerViewsContainer.MarkerViews)
            {
                var targetViewportPositionVector3 = _camera.WorldToViewportPoint(markerView.MarkerTargetPosition);
                var targetViewportPositionVector2 = new Vector2(targetViewportPositionVector3.x, targetViewportPositionVector3.y);
                var offsettedTargetViewportPosition = targetViewportPositionVector2 - Constants.OriginOffset;

                Vector2 markerPosition;
            
                if (targetViewportPositionVector3.Equals(_camera.transform.position))
                {
                    markerView.SetActiveTargetPointer(false);
                    markerPosition = new Vector2(Constants.HalfViewport, Constants.HalfViewport + EllipseRadiusY);
                }
                else
                {
                    var markerPositionOnEllipse = GetPositionOnEllipse(offsettedTargetViewportPosition);
                    var isTargetBehindCamera = targetViewportPositionVector3.z <= 0f;
                    var isTargetOffscreen = offsettedTargetViewportPosition.sqrMagnitude >= markerPositionOnEllipse.sqrMagnitude || isTargetBehindCamera;

                    markerView.SetActiveTargetPointer(isTargetOffscreen);
                
                    if (isTargetOffscreen)
                    {
                        var angle = AngleCalculatorService.GetAngle(isTargetBehindCamera, offsettedTargetViewportPosition);
                        markerView.SetTargetPointerForwardLocalRotation(angle);

                        var coefficient = isTargetBehindCamera ? Constants.NegativeCoefficient : Constants.PositiveCoefficient;
                        markerPosition = new Vector2(markerPositionOnEllipse.x, markerPositionOnEllipse.y) * coefficient + Constants.OriginOffset;
                    }
                    else
                    {
                        markerPosition = targetViewportPositionVector2;
                    }
                }

                markerView.SetMarkerPosition(markerPosition);
            }
        }

        private Vector2 GetPositionOnEllipse(Vector2 viewportPosition)
        {
            var scaledX = viewportPosition.x / EllipseRadiusX;
            var scaledY = viewportPosition.y / EllipseRadiusY;
            var length = Mathf.Sqrt(scaledX * scaledX + scaledY * scaledY);
            var normalized = new Vector2(scaledX / length, scaledY / length);
            return new Vector2(normalized.x * EllipseRadiusX, normalized.y * EllipseRadiusY);
        }

        private float EllipseRadiusX { get; set; }
        private float EllipseRadiusY { get; set; }
    }
}