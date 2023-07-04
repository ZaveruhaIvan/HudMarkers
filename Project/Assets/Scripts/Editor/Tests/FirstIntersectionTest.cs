using System.Text;
using Helpers;
using Services;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public static class FirstIntersectionTest
    {
        private static readonly Vector2 IntersectionAssert = new(7, -2);
        private static readonly Vector2 A = new(1, 4);
        private static readonly Vector2 B = new(3, 2);
        
        private const float XEquation = 7;
        private const bool IsIntersectAssert = true;

        [MenuItem("Testing/Line Intersection/First test", false, 1)]
        public static void Execute()
        {
            Debug.Log(GetResult());
        }

        public static string GetResult()
        {
            var stringBuilding = new StringBuilder();

            stringBuilding.AppendLine($"{nameof(FirstIntersectionTest).F().Y()}");
            stringBuilding.AppendLine($"Point A: {A.F()}");
            stringBuilding.AppendLine($"Point B: {B.F()}");
            stringBuilding.AppendLine($"Vertical line: X = {XEquation}");

            var intersection = LineIntersectionService.TryGetIntersectionVerticalLine(A, B, XEquation, out var isIntersect);

            var isIntersectResult = (isIntersect == IsIntersectAssert).AssertResult();
            var intersectionResult = IntersectionAssert.Equals(intersection).AssertResult();

            stringBuilding.AppendLine($"Received isIntersect {isIntersect.F()} must be equals {IsIntersectAssert.F()} {isIntersectResult}");
            stringBuilding.AppendLine($"Received intersection {intersection.F()} must be equals {IntersectionAssert.F()} {intersectionResult}");

            return stringBuilding.ToString();
        }
    }
}