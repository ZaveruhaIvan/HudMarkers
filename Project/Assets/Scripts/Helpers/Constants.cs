using UnityEngine;

namespace Helpers
{
    public static class Constants
    {
        public static readonly Vector2 OriginOffset = new(HalfViewport, HalfViewport);

        public const float MinViewport = 0f;
        public const float HalfViewport = 0.5f;
        public const float MaxViewport = 1f;
        
        public const float AngleOffset = -90f;
        
        public const int NegativeCoefficient = -1;
        public const int PositiveCoefficient = 1;
    }
}