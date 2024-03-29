using System;
using UnityEngine;

namespace Amterp.Utils {
    public static class Easing {
        public static float None(float x) {
            return x;
        }

        /** https://easings.net/#easeInSine */
        public static float EaseInSine(float x) {
            float boundedX = bound(x);
            return 1 - Mathf.Cos(Mathf.PI / 2 * boundedX);
        }

        /** https://easings.net/#easeInOutSine */
        public static float EaseInOutSine(float x) {
            float boundedX = bound(x);
            return -(Mathf.Cos(Mathf.PI * boundedX) - 1) / 2;
        }

        /** https://easings.net/#easeInQuad */
        public static float EaseInQuad(float x) {
            float boundedX = bound(x);
            return boundedX * boundedX;
        }

        /** https://easings.net/#easeInCubic */
        public static float EaseInCubic(float x) {
            float boundedX = bound(x);
            return boundedX * boundedX * boundedX;
        }

        private static float bound(float x) {
            return Mathf.Clamp(x, 0, 1);
        }
    }

    public enum EasingType {
        None,
        InSine,
        InOutSine,
        InQuad,
        InCubic,
    }

    public static class EasingTypeFunctions {
        public static float Apply(this EasingType easingType, float x) {
            switch (easingType) {
                case EasingType.None:
                    return Easing.None(x);
                case EasingType.InSine:
                    return Easing.EaseInSine(x);
                case EasingType.InOutSine:
                    return Easing.EaseInOutSine(x);
                case EasingType.InQuad:
                    return Easing.EaseInQuad(x);
                case EasingType.InCubic:
                    return Easing.EaseInCubic(x);
                default:
                    throw new InvalidOperationException("Unknown EasingType: " + easingType);
            }
        }
    }
}