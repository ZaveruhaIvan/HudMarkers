using System.Text;
using Helpers;
using Services;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public static class SecondIntersectionTest
    {
        private static readonly Vector2 IntersectionAssert = new(1.5f, 2.5f);
        private static readonly Vector2 A = new(1, 4);
        private static readonly Vector2 B = new(2, 1);
        
        private const float YEquation = 2.5f;
        private const bool IsIntersectAssert = true;

        [MenuItem("Testing/Line Intersection/Second test", false, 2)]
        public static void Execute()
        {
            Debug.Log(GetResult());
        }

        public static string GetResult()
        {
            var stringBuilding = new StringBuilder();

            stringBuilding.AppendLine($"{nameof(SecondIntersectionTest).F().Y()}");
            stringBuilding.AppendLine($"Point A: {A.F()}");
            stringBuilding.AppendLine($"Point B: {B.F()}");
            stringBuilding.AppendLine($"Horizontal line: Y = {YEquation}");

            var intersection = LineIntersectionService.TryGetIntersectionHorizontalLine(A, B, YEquation, out var isIntersect);

            var isIntersectResult = (isIntersect == IsIntersectAssert).AssertResult();
            var intersectionResult = IntersectionAssert.Equals(intersection).AssertResult();

            stringBuilding.AppendLine($"Received isIntersect {isIntersect.F()} must be equals {IsIntersectAssert.F()} {isIntersectResult}");
            stringBuilding.AppendLine($"Received intersection {intersection.F()} must be equals {IntersectionAssert.F()} {intersectionResult}");

            return stringBuilding.ToString();
        }
    }
}