using UnityEngine;

namespace Helpers
{
    public static class StringExtensions
    {
        public static string R(this string value) => $"<color=red>{value}</color>";
        public static string G(this string value) => $"<color=green>{value}</color>";
        public static string B(this string value) => $"<color=blue>{value}</color>";
        public static string Y(this string value) => $"<color=yellow>{value}</color>";
        public static string AssertResult(this bool isTrue) => isTrue ? "PASSED".F().G() : "FAILED".F().R();
        public static string F(this string value) => $"[{value}]";
        public static string F(this Vector2 value) => $"{value.x}; {value.y}".F();
        public static string F(this Vector3 value) => $"{value.x}; {value.y}; {value.z}".F();
        public static string F(this float value) => $"{value}".F();
        public static string F(this bool value) => $"{value}".F();
    }
}