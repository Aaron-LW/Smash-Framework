using System.Numerics;
using SDL3;

namespace Smash;

public class Rectangle
{
    public float XOffset;
    public float YOffset;
    public Vector2 Offset
    {
        get { return new Vector2(XOffset, YOffset); }
        set { XOffset = value.X; YOffset = value.Y; }
    }

    private float _x;
    private float _y;

    public float X { get { return _x + XOffset; } set { _x = value; }}
    public float Y { get { return _y + YOffset; } set { _y = value; }}
    public Vector2 Position
    {
        get { return new Vector2(X, Y); }
        set { X = value.X; Y = value.Y; }
    }

    private float _width;
    private float _height;

    public float Width { get { return _width; } set { _width = value; }}
    public float Height { get { return _height; } set { _height = value; }}
    public Vector2 Bounds
    {
        get { return new Vector2(Width, Height); }
        set { Width = value.X; Height = value.Y; }
    }

    public Rectangle(Vector2 position, float width, float height)
    {
        Position = position;
        Width = width;
        Height = height;
    } 

    public Rectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rectangle(Vector2 position, Vector2 bounds)
    {
        Position = position;
        Bounds = bounds;
    }

    public Rectangle(SDL.FRect sdlRect)
    {
        X = sdlRect.X;
        Y = sdlRect.Y;
        Width = sdlRect.W;
        Height = sdlRect.H;
    }

    public Rectangle() {}


    public SDL.FRect ToSDLFRect()
    {
        return new SDL.FRect
        {
            X = X,
            Y = Y,
            W = Width,
            H = Height
        };
    }

    public SDL.Rect ToSDLRect()
    {
        return new SDL.Rect
        {
            X = (int)X,
            Y = (int)Y,
            W = (int)Width,
            H = (int)Height
        };
    }

    public bool IsPositionInRectangle(Vector2 position)
    {
        return position.X >= X &&
               position.X <= X + Width &&
               position.Y >= Y &&
               position.Y <= Y + Height;
    }

    public bool IntersectsWith(Rectangle otherRectangle)
    {
        return X < otherRectangle.X + otherRectangle.Width &&
               X + Width > otherRectangle.X &&
               Y < otherRectangle.Y + otherRectangle.Height &&
               Y + Height > otherRectangle.Y;
    }

    public Vector2 GetRandomPositionInRectangle(Random random)
    {
        return new Vector2(random.Next((int)_x, (int)(_x + Width)), random.Next((int)_y, (int)(_y + Height)));
    }

    public Rectangle Clone()
    {
        return new Rectangle
        {
            _x = this._x,
            _y = this._y,
            _width = this._width,
            _height = this._height,
            Offset = this.Offset
        };
    }
}