using System.Numerics;

namespace Smash.EntityComponentSystem;

public class ColliderComponent : Component
{
    public Rectangle Rectangle;
    public bool StaticCollider = false;

    public float X { get { return Rectangle.X; } set { Rectangle.X = value; }}
    public float Y { get { return Rectangle.Y; } set { Rectangle.Y = value; }}
    public Vector2 Position { get { return Rectangle.Position; } set { Rectangle.Position = value; }}

    public float Width { get { return Rectangle.Width; } set { Rectangle.Width = value; }}
    public float Height { get { return Rectangle.Height; } set { Rectangle.Height = value; }}
    public Vector2 Bounds { get { return Rectangle.Bounds; } set { Rectangle.Bounds = value; }}

    public float Scale { get { return Rectangle.Scale; } set { Rectangle.Scale = value; }}

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
        return new ColliderComponent(Rectangle, StaticCollider);    
    }
}