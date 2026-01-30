using System.Numerics;

namespace Smash.EntityComponentSystem;

public class ColliderComponent : Component
{
    public Rectangle Rectangle;
    public bool StaticCollider = false;

    public float XOffset => Rectangle.XOffset;
    public float YOffset => Rectangle.YOffset;
    public Vector2 Offset => Rectangle.Offset;

    public float X => Rectangle.X;
    public float Y => Rectangle.Y;
    public Vector2 Position => Rectangle.Position;

    public float Width => Rectangle.Width;
    public float Height => Rectangle.Height;
    public Vector2 Bounds => Rectangle.Bounds;

    public float Scale => Rectangle.Scale;

    public ColliderComponent(Rectangle rectangle, bool staticCollider)
    {
        Rectangle = rectangle; 
        StaticCollider = staticCollider;
    }

    public ColliderComponent(float x, float y, float width, float height, float scale, bool staticCollider)
    {
        Rectangle = new Rectangle(x, y, width, height, scale);
        StaticCollider = staticCollider;
    }

    public void UpdateRectangle(TransformComponent transform)
    {
        if (StaticCollider) return;
        Rectangle.X = transform.X;
        Rectangle.Y = transform.Y;
        Rectangle.Scale = transform.Scale;
    }

    public override ColliderComponent Clone()
    {
        return new ColliderComponent(Rectangle.Clone(), StaticCollider);    
    }
}