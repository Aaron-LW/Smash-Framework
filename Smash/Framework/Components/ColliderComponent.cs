using System.Numerics;

namespace Smash.EntityComponentSystem;

public class ColliderComponent : Component
{
    public Rectangle Rectangle { get; private set; }
    public bool StaticCollider = false;

    public float X { get => Rectangle.X; set { Rectangle.X = value; }}
    public float Y { get => Rectangle.Y; set { Rectangle.Y = value; }}
    public Vector2 Position { get => Rectangle.Position; set { Rectangle.Position = value; }}

    public float Width { get => Rectangle.Width; set { Rectangle.Width = value; }}
    public float Height { get => Rectangle.Height; set { Rectangle.Height = value; }}
    public Vector2 Bounds { get => Rectangle.Bounds; set { Rectangle.Bounds = value; }}

    public ColliderComponent(Rectangle rectangle, bool staticCollider)
    {
        Rectangle = rectangle; 
        StaticCollider = staticCollider;
    }

    public ColliderComponent(float x, float y, float width, float height, bool staticCollider)
    {
        Rectangle = new Rectangle(x, y, width, height);
        StaticCollider = staticCollider;
    }

    public void UpdateRectangle(TransformComponent transform)
    {
        if (StaticCollider) return;
        Rectangle.X = transform.X;
        Rectangle.Y = transform.Y;
    }

    public override ColliderComponent Clone()
    {
        return new ColliderComponent(Rectangle.Clone(), StaticCollider);    
    }
}