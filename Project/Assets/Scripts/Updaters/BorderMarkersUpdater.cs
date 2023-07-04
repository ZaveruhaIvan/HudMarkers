using Helpers;
using Services;
using UnityEngine;
using Views;

namespace Updaters
{
    public class BorderMarkersUpdater : MonoBehaviour
    {
        [SerializeField, Header("Refs")] private Camera _camera;

        [SerializeField, Range(0.1f, 1f), Header("Settings")] private float _borderOffsetX = 0.1f;
        [SerializeField, Range(0.1f, 1f)] private float _borderOffsetY = 0.1f;

        private MarkerViewsContainer _markerViewsContainer;

        private void Awake()
        {
            MinBorderX = Constants.MinViewport + _borderOffsetX / 2;
            MaxBorderX = Constants.MaxViewport - _borderOffsetX / 2;
            MinBorderY = Constants.MinViewport + _borderOffsetY / 2;
            MaxBorderY = Constants.MaxViewport - _borderOffsetY / 2;
            OffsettedMinBorderX = MinBorderX - Constants.OriginOffset.x;
            OffsettedMaxBorderX = MaxBorderX - Constants.OriginOffset.x;
            OffsettedMinBorderY = MinBorderY - Constants.OriginOffset.y;
            OffsettedMaxBorderY = MaxBorderY - Constants.OriginOffset.y;
            OffsettedDiagonalHalf = Mathf.Sqrt(OffsettedMaxBorderX * OffsettedMaxBorderX + OffsettedMaxBorderY * OffsettedMaxBorderY);

            _markerViewsContainer = GameView.Instance.MarkerViewsContainer;
        }

        private void Update()
        {
            foreach (var markerView in _markerViewsContainer.MarkerViews)
            {
                var viewportPosV3 = _camera.WorldToViewportPoint(markerView.MarkerTargetPosition);
                var viewportPosV2 = new Vector2(viewportPosV3.x, viewportPosV3.y);
                var offsetViewportPos = viewportPosV2 - Constants.OriginOffset;

                Vector2 markerPos;

                if (viewportPosV3.Equals(_camera.transform.position))
                {
                    markerView.SetActiveTargetPointer(false);
                    markerPos = new Vector2(Constants.HalfViewport, MinBorderY);
                }
                else
                {
                    var isOffscreenX = viewportPosV3.x > MaxBorderX || viewportPosV3.x < MinBorderX;
                    var isOffscreenY = viewportPosV3.y > MaxBorderY || viewportPosV3.y < MinBorderY;
                    var isTargetBehindCamera = viewportPosV3.z <= 0f;
                    var isTargetOffscreen = isOffscreenX || isOffscreenY || isTargetBehindCamera;

                    markerView.SetActiveTargetPointer(isTargetOffscreen);
                    
                    if (isTargetOffscreen)
                    {
                        var angle = AngleCalculatorService.GetAngle(isTargetBehindCamera, offsetViewportPos);
                        markerView.SetTargetPointerForwardLocalRotation(angle);

                        var intersectionMinBorderY = LineIntersectionService.TryGetIntersectionHorizontalLine(Vector2.zero, offsetViewportPos, OffsettedMinBorderY, out var isIntersectMinBorderY);
                        var intersectionMaxBorderY = LineIntersectionService.TryGetIntersectionHorizontalLine(Vector2.zero, offsetViewportPos, OffsettedMaxBorderY, out var isIntersectMaxBorderY);
                        var intersectionMinBorderX = LineIntersectionService.TryGetIntersectionVerticalLine(Vector2.zero, offsetViewportPos, OffsettedMinBorderX, out var isIntersectMinBorderX);
                        var intersectionMaxBorderX = LineIntersectionService.TryGetIntersectionVerticalLine(Vector2.zero, offsetViewportPos, OffsettedMaxBorderX, out var isIntersectMaxBorderX);

                        var minIntersectionLength = float.MaxValue;
                        var borderPos = Vector2.zero;

                        if (isIntersectMinBorderY) CalculateBorderCoordinates(offsetViewportPos, intersectionMinBorderY, ref minIntersectionLength, ref borderPos);
                        if (isIntersectMaxBorderY) CalculateBorderCoordinates(offsetViewportPos, intersectionMaxBorderY, ref minIntersectionLength, ref borderPos);
                        if (isIntersectMinBorderX) CalculateBorderCoordinates(offsetViewportPos, intersectionMinBorderX, ref minIntersectionLength, ref borderPos);
                        if (isIntersectMaxBorderX) CalculateBorderCoordinates(offsetViewportPos, intersectionMaxBorderX, ref minIntersectionLength, ref borderPos);
                        var coefficient = isTargetBehindCamera ? Constants.NegativeCoefficient : Constants.PositiveCoefficient;
                        
                        markerPos = borderPos * coefficient + Constants.OriginOffset;
                    }
                    else
                    {
                        markerPos = viewportPosV2;
                    }
                }

                markerView.SetMarkerPosition(markerPos);
            }
        }

        private void CalculateBorderCoordinates(Vector2 targetViewportPos, Vector2 intersectPos, ref float minIntersectionLength, ref Vector2 borderPos)
        {
            var length = Vector2.Distance(targetViewportPos, intersectPos);
            var intersectLength = Vector2.Distance(Vector2.zero, intersectPos);

            if (length < minIntersectionLength && intersectLength < OffsettedDiagonalHalf)
            {
                minIntersectionLength = length;

                var clampedX = Mathf.Clamp(intersectPos.x, OffsettedMinBorderX, OffsettedMaxBorderX);
                var clampedY = Mathf.Clamp(intersectPos.y, OffsettedMinBorderY, OffsettedMaxBorderY);
                borderPos = new Vector2(clampedX, clampedY);
            }
        }

        private float MinBorderX { get; set; }
        private float MaxBorderX { get; set; }
        private float MinBorderY { get; set; }
        private float MaxBorderY { get; set; }
        private float OffsettedMinBorderX { get; set; }
        private float OffsettedMaxBorderX { get; set; }
        private float OffsettedMinBorderY { get; set; }
        private float OffsettedMaxBorderY { get; set; }
        private float OffsettedDiagonalHalf { get; set; }
    }
}