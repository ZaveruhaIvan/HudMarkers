using System.Text;
using Helpers;
using Services;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public static class ThirdIntersectionTest
    {
        private static readonly Vector2 A = new(-3, 3);
        private static readonly Vector2 B = new(4, 3);
        
        private const float YEquation = 1;
        private const bool IsIntersectAssert = false;

        [MenuItem("Testing/Line Intersection/Third test", false, 3)]
        public static void Execute()
        {
            Debug.Log(GetResult());
        }

        public static string GetResult()
        {
            var stringBuilding = new StringBuilder();

            stringBuilding.AppendLine($"{nameof(ThirdIntersectionTest).F().Y()}");
            stringBuilding.AppendLine($"Point A: {A.F()}");
            stringBuilding.AppendLine($"Point B: {B.F()}");
            stringBuilding.AppendLine($"Vertical line: X = {YEquation}");

            LineIntersectionService.TryGetIntersectionHorizontalLine(A, B, YEquation, out var isIntersect);
            var isIntersectResult = (isIntersect == IsIntersectAssert).AssertResult();

            stringBuilding.AppendLine($"Received isIntersect {isIntersect.F()} must be equals {IsIntersectAssert.F()} {isIntersectResult}");
            return stringBuilding.ToString();
        }
    }
}