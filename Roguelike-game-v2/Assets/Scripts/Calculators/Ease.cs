using UnityEngine;
/// <summary>
/// Tweening에 사용되는 Ease그래프의 식 구현
/// </summary>
public static class Ease
{
    private const float Start = 0;
    private const float End = 1;

    public static float Linear(float value)
    {
        return Mathf.Lerp(Start, End, value);
    }
    public static float InQuad(float value)
    {
        return value * value;
    }
    public static float OutQuad(float value)
    {
        return -value * (value - 2);
    }
    public static float InOutQuad(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return 0.5f * value * value;
        }

        value--;

        return -0.5f * (value * (value - 2) - 1);
    }
    public static float InCubic(float value)
    {
        return value * value * value;
    }
    public static float OutCubic(float value)
    {
        value--;

        return (value * value * value + 1);
    }
    public static float InOutCubic(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return 0.5f * value * value * value;
        }

        value -= 2;

        return 0.5f * (value * value * value + 2);
    }
    public static float InQuart(float value)
    {
        return value * value * value * value;
    }
    public static float OutQuart(float value)
    {
        value--;

        return -(value * value * value * value - 1);
    }
    public static float InOutQuart(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return 0.5f * value * value * value * value;
        }

        value -= 2;

        return -0.5f * (value * value * value * value - 2);
    }
    public static float InQuint(float value)
    {
        return value * value * value * value * value;
    }
    public static float OutQuint(float value)
    {
        value--;

        return (value * value * value * value * value + 1);
    }
    public static float InOutQuint(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return 0.5f * value * value * value * value * value;
        }

        value -= 2;

        return 0.5f * (value * value * value * value * value + 2);
    }
    public static float InSine(float value)
    {
        return -Mathf.Cos(value * (Mathf.PI * 0.5f)) + End;
    }
    public static float OutSine(float value)
    {
        return Mathf.Sin(value * (Mathf.PI * 0.5f));
    }
    public static float InOutSine(float value)
    {
        return -0.5f * (Mathf.Cos(Mathf.PI * value) - 1);
    }
    public static float InExpo(float value)
    {
        return Mathf.Pow(2, 10 * (value - 1));
    }
    public static float OutExpo(float value)
    {
        return (-Mathf.Pow(2, -10 * value) + 1);
    }
    public static float InOutExpo(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return 0.5f * Mathf.Pow(2, 10 * (value - 1));
        }

        value--;

        return 0.5f * (-Mathf.Pow(2, -10 * value) + 2);
    }
    public static float InCirc(float value)
    {
        return -(Mathf.Sqrt(1 - value * value) - 1);
    }
    public static float OutCirc(float value)
    {
        value--;

        return Mathf.Sqrt(1 - value * value);
    }
    public static float InOutCirc(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return -0.5f * (Mathf.Sqrt(1 - value * value) - 1);
        }

        value -= 2;

        return 0.5f * (Mathf.Sqrt(1 - value * value) + 1);
    }
    public static float InBounce(float value)
    {
        float d = 1f;

        return End - OutBounce(d - value);
    }
    public static float OutBounce(float value)
    {
        if(value < (1 / 2.75f))
        {
            return (7.5625f * value * value);
        }
        else if(value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);

            return (7.5625f * (value) * value + .75f);
        }
        else if(value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);

            return (7.5625f * (value) * value + .9375f);
        }
        else
        {
            value -= (2.625f / 2.75f);

            return (7.5625f * (value) * value + .984375f);
        }
    }
    public static float InOutBounce(float value)
    {
        float d = 1f;

        if(value < d * 0.5f)
        {
            return InBounce(value * 2) * 0.5f;
        }
        else
        {
            return OutBounce(value * 2 - d) * 0.5f + 0.5f;
        }
    }
    public static float InBack(float value)
    {
        float s = 1.70158f;

        value /= 1;

        return (value) * value * ((s + 1) * value - s);
    }
    public static float OutBack(float value)
    {
        float s = 1.70158f;

        value = (value) - 1;

        return ((value) * value * ((s + 1) * value + s) + 1);
    }
    public static float InOutBack(float value)
    {
        float s = 1.70158f;

        value /= 0.5f;

        if((value) < 1)
        {
            s *= 1.525f;

            return 0.5f * (value * value * (((s) + 1) * value - s));
        }

        value -= 2;
        s *= 1.525f;

        return 0.5f * ((value) * value * (((s) + 1) * value + s) + 2);
    }
    public static float InElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return Start;
        }

        if((value /= d) == 1)
        {
            return End;
        }

        if(a == 0f || a < Mathf.Abs(End))
        {
            a = End;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(End / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p));
    }
    public static float OutElastic(float value)
    {
        float d = 1f;
        float p = d * 0.3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return Start;
        }

        if((value /= d) == 1)
        {
            return End;
        }

        if(a == 0f || a < Mathf.Abs(End))
        {
            a = End;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(End / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + End);
    }
    public static float InOutElastic(float value)
    {
        float d = 1f;
        float p = d * 0.3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return Start;
        }

        if((value /= d * 0.5f) == 2)
        {
            return End;
        }

        if(a == 0f || a < Mathf.Abs(End))
        {
            a = End;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(End / a);
        }

        if(value < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p));
        }

        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + End;
    }

    //Additional
    public static float AcceleratedFall(float value)
    {
        return Mathf.Lerp(Start, End, value * value);
    }
}