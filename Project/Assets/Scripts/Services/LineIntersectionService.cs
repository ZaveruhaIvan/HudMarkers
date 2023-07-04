using System;
using UnityEngine;

namespace Services
{
    public class LineIntersectionService
    {
        public static Vector2 TryGetIntersectionHorizontalLine(Vector2 pointA, Vector2 pointB, float yEquation, out bool isIntersect)
        {
            if (Math.Abs(pointA.y - pointB.y) < float.Epsilon || pointA == pointB)
            {
                isIntersect = false;
                return Vector2.zero;
            }

            isIntersect = true;

            var coef = (pointB.x - pointA.x) / (pointB.y - pointA.y);
            var x = coef * yEquation - pointA.y * coef + pointA.x;
            return new Vector2(x, yEquation);
        }

        public static Vector2 TryGetIntersectionVerticalLine(Vector2 pointA, Vector2 pointB, float xEquation, out bool isIntersect)
        {
            if (Math.Abs(pointA.x - pointB.x) < float.Epsilon || pointA == pointB)
            {
                isIntersect = false;
                return Vector2.zero;
            }

            isIntersect = true;

            var coef = (pointB.y - pointA.y) / (pointB.x - pointA.x);
            var y = coef * xEquation - pointA.x * coef + pointA.y;
            return new Vector2(xEquation, y);
        }
    }
}