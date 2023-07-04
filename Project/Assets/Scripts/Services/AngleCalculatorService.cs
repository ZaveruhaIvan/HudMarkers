using Helpers;
using UnityEngine;

namespace Services
{
    public static class AngleCalculatorService
    {
        public static float GetAngle(bool isTargetBehindCamera, Vector2 viewportPosition)
        {
            var y = isTargetBehindCamera ? viewportPosition.y : -viewportPosition.y;
            var x = isTargetBehindCamera ? viewportPosition.x : -viewportPosition.x;
            return Mathf.Atan2(y, x) * Mathf.Rad2Deg - Constants.AngleOffset;
        }
    }
}