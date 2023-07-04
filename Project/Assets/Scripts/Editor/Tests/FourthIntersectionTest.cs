using System.Text;
using Helpers;
using Services;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public static class FourthIntersectionTest
    {
        private static readonly Vector2 A = new(3, 2);
        private static readonly Vector2 B = new(3, -5);
        
        private const float XEquation = -1;
        private const bool IsIntersectAssert = false;

        [MenuItem("Testing/Line Intersection/Fourth test", false, 4)]
        public static void Execute()
        {
            Debug.Log(GetResult());
        }

        public static string GetResult()
        {
            var stringBuilding = new StringBuilder();

            stringBuilding.AppendLine($"{nameof(FourthIntersectionTest).F().Y()}");
            stringBuilding.AppendLine($"Point A: {A.F()}");
            stringBuilding.AppendLine($"Point B: {B.F()}");
            stringBuilding.AppendLine($"Vertical line: X = {XEquation}");

            LineIntersectionService.TryGetIntersectionVerticalLine(A, B, XEquation, out var isIntersect);
            var isIntersectResult = (isIntersect == IsIntersectAssert).AssertResult();

            stringBuilding.AppendLine($"Received isIntersect {isIntersect.F()} must be equals {IsIntersectAssert.F()} {isIntersectResult}");

            return stringBuilding.ToString();
        }
    }
}