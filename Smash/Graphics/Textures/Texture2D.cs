using System.Numerics;
using SDL3;

namespace Smash.Graphics;

public class Texture2D
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector2 Bounds => new Vector2(Width, Height);
    
    public nint Handle;

    public Texture2D(nint textureHandle)
    {
        Handle = textureHandle;
        SDL.GetTextureSize(Handle, out float width, out float height);
        Width = width;
        Height = height;
    }
}