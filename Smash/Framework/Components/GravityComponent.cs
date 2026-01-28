using System.Numerics;

namespace Smash.EntityComponentSystem;

public class GravityComponent : Component
{
    public float Gravity;
    public float YAcceleration;

    public GravityComponent(float gravity)
    {
        Gravity = gravity;
    }

    public override Component Clone()
    {
        return new GravityComponent(Gravity);
    }
}