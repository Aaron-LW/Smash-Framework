using System.Numerics;
using SDL3;

namespace Smash.Graphics;

public class Texture2D
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    
    /// <summary>
    /// The combined width and height of this Texture2D
    /// </summary>
    public Vector2 Bounds => new Vector2(Width, Height);
    
    /// <summary>
    /// The handle of this Texture2D instance
    /// </summary>
    public nint Handle;

    public Texture2D(nint textureHandle)
    {
        Handle = textureHandle;
        SDL.GetTextureSize(Handle, out float width, out float height);
        Width = width;
        Height = height;
    }
}