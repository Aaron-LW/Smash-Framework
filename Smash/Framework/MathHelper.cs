using System.Numerics;

public static class MathHelper
{
    public static float Lerp(float startValue, float endValue, float time)
    {
        return startValue + (endValue - startValue) * time;
    }

    public static Vector2 LerpVector(Vector2 startValue, Vector2 endValue, float time)
    {
        return startValue + (endValue - startValue) * time;
    }
}