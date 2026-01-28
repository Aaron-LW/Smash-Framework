using System.Numerics;

namespace Smash.EntityComponentSystem;

public class TransformComponent : Component
{
    public float X;
    public float Y;
    public Vector2 Position
    {
        get { return new Vector2(X, Y); }
        set { X = value.X; Y = value.Y; }
    }

    public float Scale;

    public TransformComponent(float x, float y, float scale)
    {
        X = x;
        Y = y;
        Scale = scale;
    }

    public TransformComponent(Vector2 position, float scale)
    {
        Position = position;
        Scale = scale;
    }

    public override TransformComponent Clone()
    {
        return new TransformComponent(X, Y, Scale);
    }
}