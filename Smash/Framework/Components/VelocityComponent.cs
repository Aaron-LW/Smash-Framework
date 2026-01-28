using System.Numerics;

namespace Smash.EntityComponentSystem;

public class VelocityComponent : Component
{
    public float X;
    public float Y;
    public Vector2 Velocity
    {
        get { return new Vector2(X, Y); }
        set { X = value.X; Y = value.Y; }
    }

    public VelocityComponent(Vector2? initialVelocity = null)
    {
        if (initialVelocity != null) Velocity = (Vector2)initialVelocity;
    }

    public VelocityComponent(float? x = null, float? y = null)
    {
        if (x != null) X = (float)x;
        if (y != null) Y = (float)y;
    }

    public override Component Clone()
    {
        return new VelocityComponent(Velocity);
    }
}