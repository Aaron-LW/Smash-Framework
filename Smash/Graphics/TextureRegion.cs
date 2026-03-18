using System.Numerics;

namespace Smash.Graphics;

public class TextureRegion
{
    public readonly string BaseTextureName;

    public readonly int X;
    public readonly int Y;
    
    public readonly int Width;
    public readonly int Height;
    public Vector2 Bounds => new Vector2(Width, Height);

    public TextureRegion(string baseTextureName, int x, int y, int widht, int height)
    {
        X = x;
        Y = y;
        Width = widht;
        Height = height;
        BaseTextureName = baseTextureName;
    }
}