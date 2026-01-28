using System.Numerics;

namespace Smash.Graphics;

public class Camera
{
    public float X;
    public float Y;
    public Vector2 Position
    {
        get { return new Vector2(X, Y); }
        set { X = value.X; Y = value.Y; }
    }

    public float Zoom;
}