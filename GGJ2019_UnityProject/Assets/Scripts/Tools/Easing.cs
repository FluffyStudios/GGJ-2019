using UnityEngine;

namespace FluffyTools
{
    public static class Easing
    {
        public delegate float EasingFunction(float start, float end, float value);

        public static EasingFunction GetEasingFunction(EaseType easeType)
        {
            switch (easeType)
            {
                case EaseType.EaseInQuad:       return EaseInQuad;
                case EaseType.EaseOutQuad:      return EaseOutQuad;
                case EaseType.EaseInOutQuad:    return EaseInOutQuad;
                case EaseType.EaseInCubic:      return EaseInCubic;
                case EaseType.EaseOutCubic:     return EaseOutCubic;
                case EaseType.EaseInOutCubic:   return EaseInOutCubic;
                case EaseType.EaseInQuart:      return EaseInQuart;
                case EaseType.EaseOutQuart:     return EaseOutQuart;
                case EaseType.EaseInOutQuart:   return EaseInOutQuart;
                case EaseType.EaseInQuint:      return EaseInQuint;
                case EaseType.EaseOutQuint:     return EaseOutQuint;
                case EaseType.EaseInOutQuint:   return EaseInOutQuint;
                case EaseType.EaseInSine:       return EaseInSine;
                case EaseType.EaseOutSine:      return EaseOutSine;
                case EaseType.EaseInOutSine:    return EaseInOutSine;
                case EaseType.EaseInExpo:       return EaseInExpo;
                case EaseType.EaseOutExpo:      return EaseOutExpo;
                case EaseType.EaseInOutExpo:    return EaseInOutExpo;
                case EaseType.EaseInCirc:       return EaseInCirc;
                case EaseType.EaseOutCirc:      return EaseOutCirc;
                case EaseType.EaseInOutCirc:    return EaseInOutCirc;
                case EaseType.Linear:           return Linear;
                case EaseType.Spring:           return Spring;
                case EaseType.EaseInBounce:     return EaseInBounce;
                case EaseType.EaseOutBounce:    return EaseOutBounce;
                case EaseType.EaseInOutBounce:  return EaseInOutBounce;
                case EaseType.EaseInBack:       return EaseInBack;
                case EaseType.EaseOutBack:      return EaseOutBack;
                case EaseType.EaseInOutBack:    return EaseInOutBack;
                case EaseType.EaseInElastic:    return EaseInElastic;
                case EaseType.EaseOutElastic:   return EaseOutElastic;
                case EaseType.EaseInOutElastic: return EaseInOutElastic;
            }

            throw new System.NotImplementedException();
        }

        public static EasingFunction GetEasingDerivativeFunction(EaseType easeType)
        {
            switch (easeType)
            {
            case EaseType.EaseInQuad:       return EaseInQuadD;
            case EaseType.EaseOutQuad:      return EaseOutQuadD;
            case EaseType.EaseInOutQuad:    return EaseInOutQuadD;
            case EaseType.EaseInCubic:      return EaseInCubicD;
            case EaseType.EaseOutCubic:     return EaseOutCubicD;
            case EaseType.EaseInOutCubic:   return EaseInOutCubicD;
            case EaseType.EaseInQuart:      return EaseInQuartD;
            case EaseType.EaseOutQuart:     return EaseOutQuartD;
            case EaseType.EaseInOutQuart:   return EaseInOutQuartD;
            case EaseType.EaseInQuint:      return EaseInQuintD;
            case EaseType.EaseOutQuint:     return EaseOutQuintD;
            case EaseType.EaseInOutQuint:   return EaseInOutQuintD;
            case EaseType.EaseInSine:       return EaseInSineD;
            case EaseType.EaseOutSine:      return EaseOutSineD;
            case EaseType.EaseInOutSine:    return EaseInOutSineD;
            case EaseType.EaseInExpo:       return EaseInExpoD;
            case EaseType.EaseOutExpo:      return EaseOutExpoD;
            case EaseType.EaseInOutExpo:    return EaseInOutExpoD;
            case EaseType.EaseInCirc:       return EaseInCircD;
            case EaseType.EaseOutCirc:      return EaseOutCircD;
            case EaseType.EaseInOutCirc:    return EaseInOutCircD;
            case EaseType.Linear:           return LinearD;
            case EaseType.Spring:           return SpringD;
            case EaseType.EaseInBounce:     return EaseInBounceD;
            case EaseType.EaseOutBounce:    return EaseOutBounceD;
            case EaseType.EaseInOutBounce:  return EaseInOutBounceD;
            case EaseType.EaseInBack:       return EaseInBackD;
            case EaseType.EaseOutBack:      return EaseOutBackD;
            case EaseType.EaseInOutBack:    return EaseInOutBackD;
            case EaseType.EaseInElastic:    return EaseInElasticD;
            case EaseType.EaseOutElastic:   return EaseOutElasticD;
            case EaseType.EaseInOutElastic: return EaseInOutElasticD;
            }

            throw new System.NotImplementedException();
        }

        public enum EaseType
        {
            EaseInQuad,
            EaseOutQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseOutCubic,
            EaseInOutCubic,
            EaseInQuart,
            EaseOutQuart,
            EaseInOutQuart,
            EaseInQuint,
            EaseOutQuint,
            EaseInOutQuint,
            EaseInSine,
            EaseOutSine,
            EaseInOutSine,
            EaseInExpo,
            EaseOutExpo,
            EaseInOutExpo,
            EaseInCirc,
            EaseOutCirc,
            EaseInOutCirc,
            Linear,
            Spring,
            EaseInBounce,
            EaseOutBounce,
            EaseInOutBounce,
            EaseInBack,
            EaseOutBack,
            EaseInOutBack,
            EaseInElastic,
            EaseOutElastic,
            EaseInOutElastic
        }

        #region Easing Curves
        private static float Linear(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value);
        }

        private static float Spring(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }

        private static float EaseInQuad(float start, float end, float value)
        {
            end -= start;
            return end * value * value + start;
        }

        private static float EaseOutQuad(float start, float end, float value)
        {
            end -= start;
            return -end * value * (value - 2f) + start;
        }

        private static float EaseInOutQuad(float start, float end, float value)
        {
            value *= 2f;
            end -= start;
            if (value < 1f) return end * 0.5f * value * value + start;
            value--;
            return -end * 0.5f * (value * (value - 2f) - 1f) + start;
        }

        private static float EaseInCubic(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value + start;
        }

        private static float EaseOutCubic(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value + 1f) + start;
        }

        private static float EaseInOutCubic(float start, float end, float value)
        {
            value *= 2f;
            end -= start;
            if (value < 1f) return end * 0.5f * value * value * value + start;
            value -= 2f;
            return end * 0.5f * (value * value * value + 2f) + start;
        }

        private static float EaseInQuart(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value + start;
        }

        private static float EaseOutQuart(float start, float end, float value)
        {
            value--;
            end -= start;
            return -end * (value * value * value * value - 1f) + start;
        }

        private static float EaseInOutQuart(float start, float end, float value)
        {
            value *= 2f;
            end -= start;
            if (value < 1f) return end * 0.5f * value * value * value * value + start;
            value -= 2f;
            return -end * 0.5f * (value * value * value * value - 2f) + start;
        }

        private static float EaseInQuint(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value * value * value + start;
        }

        private static float EaseOutQuint(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * (value * value * value * value * value + 1f) + start;
        }

        private static float EaseInOutQuint(float start, float end, float value)
        {
            value *= 2f;
            end -= start;
            if (value < 1f) return end * 0.5f * value * value * value * value * value + start;
            value -= 2f;
            return end * 0.5f * (value * value * value * value * value + 2f) + start;
        }

        private static float EaseInSine(float start, float end, float value)
        {
            end -= start;
            return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
        }

        private static float EaseOutSine(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
        }

        private static float EaseInOutSine(float start, float end, float value)
        {
            end -= start;
            return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1f) + start;
        }

        private static float EaseInExpo(float start, float end, float value)
        {
            end -= start;
            return end * Mathf.Pow(2, 10 * (value - 1f)) + start;
        }

        private static float EaseOutExpo(float start, float end, float value)
        {
            end -= start;
            return end * (-Mathf.Pow(2, -10 * value) + 1f) + start;
        }

        private static float EaseInOutExpo(float start, float end, float value)
        {
            value *= 2f;
            end -= start;
            if (value < 1f) return end * 0.5f * Mathf.Pow(2, 10 * (value - 1f)) + start;
            value--;
            return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2f) + start;
        }

        private static float EaseInCirc(float start, float end, float value)
        {
            end -= start;
            return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
        }

        private static float EaseOutCirc(float start, float end, float value)
        {
            value--;
            end -= start;
            return end * Mathf.Sqrt(1f - value * value) + start;
        }

        private static float EaseInOutCirc(float start, float end, float value)
        {
            value *= 2f;
            end -= start;
            if (value < 1f) return -end * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
            value -= 2f;
            return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
        }

        private static float EaseInBounce(float start, float end, float value)
        {
            end -= start;
            float d = 1f;
            return end - EaseOutBounce(0f, end, d - value) + start;
        }

        private static float EaseOutBounce(float start, float end, float value)
        {
            end -= start;
            if (value < (1f / 2.75f))
            {
                return end * (7.5625f * value * value) + start;
            }
            else if (value < (2f / 2.75f))
            {
                value -= (1.5f / 2.75f);
                return end * (7.5625f * value * value + 0.75f) + start;
            }
            else if (value < (2.5f / 2.75f))
            {
                value -= (2.25f / 2.75f);
                return end * (7.5625f * value * value + 0.9375f) + start;
            }
            else
            {
                value -= (2.625f / 2.75f);
                return end * (7.5625f * value * value + 0.984375f) + start;
            }
        }

        private static float EaseInOutBounce(float start, float end, float value)
        {
            end -= start;
            float d = 1f;
            if (value < d * 0.5f) return EaseInBounce(0f, end, value * 2f) * 0.5f + start;
            else return EaseOutBounce(0f, end, value * 2f - d) * 0.5f + end * 0.5f + start;
        }

        private static float EaseInBack(float start, float end, float value)
        {
            end -= start;
            float s = 1.70158f;
            return end * value * value * ((s + 1f) * value - s) + start;
        }

        private static float EaseOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value = (value) - 1f;
            return end * (value * value * ((s + 1f) * value + s) + 1f) + start;
        }

        private static float EaseInOutBack(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value *= 2f;
            if (value < 1f)
            {
                s *= 1.525f;
                return end * 0.5f * (value * value * ((s + 1f) * value - s)) + start;
            }
            value -= 2f;
            s *= 1.525f;
            return end * 0.5f * (value * value * ((s + 1f) * value + s) + 2f) + start;
        }

        private static float EaseInElastic(float start, float end, float value)
        {
            end -= start;

            float p = 0.3f;
            float s;
            float a = 0f;

            if (value == 0f) return start;

            if (value == 1f) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
            }

            return -(a * Mathf.Pow(2, 10 * (value -= 1f)) * Mathf.Sin((value - s) * (2f * Mathf.PI) / p)) + start;
        }

        private static float EaseOutElastic(float start, float end, float value)
        {
            end -= start;

            float p = 0.3f;
            float s;
            float a = 0f;

            if (value == 0f) return start;

            if (value == 1f) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
            }

            return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value - s) * (2 * Mathf.PI) / p) + end + start);
        }

        private static float EaseInOutElastic(float start, float end, float value)
        {
            end -= start;

            float p = 0.3f;
            float s;
            float a = 0f;

            if (value == 0f) return start;

            if ((value *= 2f) == 2f) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
            }

            if (value < 1f) return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1f)) * Mathf.Sin((value - s) * (2f * Mathf.PI) / p)) + start;
            return a * Mathf.Pow(2, -10 * (value -= 1f)) * Mathf.Sin((value - s) * (2f * Mathf.PI) / p) * 0.5f + end + start;
        }
        #endregion

        #region Derivative Curves
        //
        // These are derived functions that the motor can use to get the speed at a specific time.
        //
        // The easing functions all work with a normalized time (0 to 1) and the returned value here
        // reflects that. Values returned here should be divided by the actual time.
        //
        // TODO: These functions have not had the testing they deserve.

        private const float NATURAL_LOG_OF_2 = 0.693147181f;

        private static float LinearD(float start, float end, float value)
        {
            return end - start;
        }

        private static float EaseInQuadD(float start, float end, float value)
        {
            return 2f * (end - start) * value;
        }

        private static float EaseOutQuadD(float start, float end, float value)
        {
            end -= start;
            return -end * value - end * (value - 2f);
        }

        private static float EaseInOutQuadD(float start, float end, float value)
        {
            value *= 2f;
            end -= start;

            if (value < 1f)
            {
                return end * value;
            }

            value--;

            return end * (1f - value);
        }

        private static float EaseInCubicD(float start, float end, float value)
        {
            return 3f * (end - start) * value * value;
        }

        private static float EaseOutCubicD(float start, float end, float value)
        {
            value--;
            end -= start;
            return 3f * end * value * value;
        }

        private static float EaseInOutCubicD(float start, float end, float value)
        {
            value *= 2f;
            end -= start;

            if (value < 1f)
            {
                return (3f / 2f) * end * value * value;
            }

            value -= 2f;

            return (3f / 2f) * end * value * value;
        }

        private static float EaseInQuartD(float start, float end, float value)
        {
            return 4f * (end - start) * value * value * value;
        }

        private static float EaseOutQuartD(float start, float end, float value)
        {
            value--;
            end -= start;
            return -4f * end * value * value * value;
        }

        private static float EaseInOutQuartD(float start, float end, float value)
        {
            value *= 2f;
            end -= start;

            if (value < 1f)
            {
                return 2f * end * value * value * value;
            }

            value -= 2f;

            return -2f * end * value * value * value;
        }

        private static float EaseInQuintD(float start, float end, float value)
        {
            return 5f * (end - start) * value * value * value * value;
        }

        private static float EaseOutQuintD(float start, float end, float value)
        {
            value--;
            end -= start;
            return 5f * end * value * value * value * value;
        }

        private static float EaseInOutQuintD(float start, float end, float value)
        {
            value *= 2f;
            end -= start;

            if (value < 1f)
            {
                return (5f / 2f) * end * value * value * value * value;
            }

            value -= 2f;

            return (5f / 2f) * end * value * value * value * value;
        }

        private static float EaseInSineD(float start, float end, float value)
        {
            return (end - start) * 0.5f * Mathf.PI * Mathf.Sin(0.5f * Mathf.PI * value);
        }

        private static float EaseOutSineD(float start, float end, float value)
        {
            end -= start;
            return (Mathf.PI * 0.5f) * end * Mathf.Cos(value * (Mathf.PI * 0.5f));
        }

        private static float EaseInOutSineD(float start, float end, float value)
        {
            end -= start;
            return end * 0.5f * Mathf.PI * Mathf.Sin(Mathf.PI * value);
        }
        private static float EaseInExpoD(float start, float end, float value)
        {
            return (10f * NATURAL_LOG_OF_2 * (end - start) * Mathf.Pow(2f, 10f * (value - 1f)));
        }

        private static float EaseOutExpoD(float start, float end, float value)
        {
            end -= start;
            return 5f * NATURAL_LOG_OF_2 * end * Mathf.Pow(2f, 1f - 10f * value);
        }

        private static float EaseInOutExpoD(float start, float end, float value)
        {
            value *= 2f;
            end -= start;

            if (value < 1f)
            {
                return 5f * NATURAL_LOG_OF_2 * end * Mathf.Pow(2f, 10f * (value - 1f));
            }

            value--;

            return (5f * NATURAL_LOG_OF_2 * end) / (Mathf.Pow(2f, 10f * value));
        }

        private static float EaseInCircD(float start, float end, float value)
        {
            return ((end - start) * value) / Mathf.Sqrt(1f - value * value);
        }

        private static float EaseOutCircD(float start, float end, float value)
        {
            value--;
            end -= start;
            return (-end * value) / Mathf.Sqrt(1f - value * value);
        }

        private static float EaseInOutCircD(float start, float end, float value)
        {
            value *= 2f;
            end -= start;

            if (value < 1f)
            {
                return (end * value) / (2f * Mathf.Sqrt(1f - value * value));
            }

            value -= 2f;

            return (-end * value) / (2f * Mathf.Sqrt(1f - value * value));
        }

        private static float EaseInBounceD(float start, float end, float value)
        {
            end -= start;
            return EaseOutBounceD(0, end, 1f - value);
        }

        private static float EaseOutBounceD(float start, float end, float value)
        {
            end -= start;

            if (value < (1f / 2.75f))
            {
                return 2f * end * 7.5625f * value;
            }
            else if (value < (2f / 2.75f))
            {
                value -= (1.5f / 2.75f);
                return 2f * end * 7.5625f * value;
            }
            else if (value < (2.5f / 2.75f))
            {
                value -= (2.25f / 2.75f);
                return 2f * end * 7.5625f * value;
            }
            else
            {
                value -= (2.625f / 2.75f);
                return 2f * end * 7.5625f * value;
            }
        }

        private static float EaseInOutBounceD(float start, float end, float value)
        {
            end -= start;
            if (value < 0.5f)
            {
                return EaseInBounceD(0f, end, value * 2f) * 0.5f;
            }
            else
            {
                return EaseOutBounceD(0f, end, value * 2f - 1f) * 0.5f;
            }
        }

        private static float EaseInBackD(float start, float end, float value)
        {
            float s = 1.70158f;

            return 3f * (s + 1f) * (end - start) * value * value - 2f * s * (end - start) * value;
        }

        private static float EaseOutBackD(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value = value - 1f;

            return end * ((s + 1f) * value * value + 2f * value * ((s + 1f) * value + s));
        }

        private static float EaseInOutBackD(float start, float end, float value)
        {
            float s = 1.70158f;
            end -= start;
            value *= 2f;

            if (value < 1f)
            {
                s *= 1.525f;
                return 0.5f * end * (s + 1f) * value * value + end * value * ((s + 1f) * value - s);
            }

            value -= 2f;
            s *= 1.525f;
            return 0.5f * end * ((s + 1f) * value * value + 2f * value * ((s + 1f) * value + s));
        }

        private static float EaseInElasticD(float start, float end, float value)
        {
            end -= start;

            float p = 0.3f;
            float s;
            float a = 0f;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
            }

            float c = 2f * Mathf.PI;

            // From an online derivative calculator, kinda hoping it is right.
            return ((-a) * c * Mathf.Cos((c * ((value - 1f) - s)) / p)) / p -
                5f * NATURAL_LOG_OF_2 * a * Mathf.Sin((c * ((value - 1f) - s)) / p) *
                Mathf.Pow(2f, 10f * (value - 1f) + 1f);
        }

        private static float EaseOutElasticD(float start, float end, float value)
        {
            end -= start;

            float p = 0.3f;
            float s;
            float a = 0f;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
            }

            return (a * Mathf.PI * Mathf.Pow(2f, 1f - 10f * value) *
                Mathf.Cos((2f * Mathf.PI * (value - s)) / p)) / p - 5f * NATURAL_LOG_OF_2 * a *
                Mathf.Pow(2f, 1f - 10f * value) * Mathf.Sin((2f * Mathf.PI * (value - s)) / p);
        }

        private static float EaseInOutElasticD(float start, float end, float value)
        {
            end -= start;

            float p = 0.3f;
            float s;
            float a = 0f;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
            }

            if (value < 1f)
            {
                value -= 1f;

                return -5f * NATURAL_LOG_OF_2 * a * Mathf.Pow(2f, 10f * value) * Mathf.Sin(2 * Mathf.PI * (value - 2f) / p) -
                    a * Mathf.PI * Mathf.Pow(2f, 10f * value) * Mathf.Cos(2f * Mathf.PI * (value - s) / p) / p;
            }

            value -= 1f;

            return a * Mathf.PI * Mathf.Cos(2f * Mathf.PI * (value - s) / p) / (p * Mathf.Pow(2f, 10f * value)) -
                5f * NATURAL_LOG_OF_2 * a * Mathf.Sin(2f * Mathf.PI * (value - s) / p) / (Mathf.Pow(2f, 10f * value));
        }

        private static float SpringD(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            end -= start;

            // Damn... Thanks http://www.derivative-calculator.net/
            return end * (6f * (1f - value) / 5f + 1f) * (-2.2f * Mathf.Pow(1f - value, 1.2f) *
                Mathf.Sin(Mathf.PI * value * (2.5f * value * value * value + 0.2f)) + Mathf.Pow(1f - value, 2.2f) *
                (Mathf.PI * (2.5f * value * value * value + 0.2f) + 7.5f * Mathf.PI * value * value * value) *
                Mathf.Cos(Mathf.PI * value * (2.5f * value * value * value + 0.2f)) + 1f) -
                6f * end * (Mathf.Pow(1 - value, 2.2f) * Mathf.Sin(Mathf.PI * value * (2.5f * value * value * value + 0.2f)) + value
                    / 5f);

        }
        #endregion
    }
}