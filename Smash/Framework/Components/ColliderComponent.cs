using System.Numerics;

namespace Smash.EntityComponentSystem;

public class ColliderComponent : Component
{
    public Rectangle Rectangle { get; private set; }
    public bool StaticCollider = false;

    public float XOffset { get => Rectangle.XOffset; set { Rectangle.XOffset = value; }}
    public float YOffset { get => Rectangle.YOffset; set { Rectangle.YOffset = value; }}
    public Vector2 Offset { get => Rectangle.Offset; set { Rectangle.Offset = value; }}

    public float X { get => Rectangle.X; set { Rectangle.X = value; }}
    public float Y { get => Rectangle.Y; set { Rectangle.Y = value; }}
    public Vector2 Position { get => Rectangle.Position; set { Rectangle.Position = value; }}

    public float Width { get => Rectangle.Width; set { Rectangle.Width = value; }}
    public float Height { get => Rectangle.Height; set { Rectangle.Height = value; }}
    public Vector2 Bounds { get => Rectangle.Bounds; set { Rectangle.Bounds = value; }}

    public float Scale { get => Rectangle.Scale; set { Rectangle.Scale = value; }}

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