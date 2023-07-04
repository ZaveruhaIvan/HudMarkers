using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public static class AllIntersectionTest
    {
        [MenuItem("Testing/Line Intersection/All tests", false, 0)]
        public static void Execute()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(FirstIntersectionTest.GetResult());
            stringBuilder.AppendLine(SecondIntersectionTest.GetResult());
            stringBuilder.AppendLine(ThirdIntersectionTest.GetResult());
            stringBuilder.AppendLine(FourthIntersectionTest.GetResult());
            Debug.Log(stringBuilder);
        }
    }
}