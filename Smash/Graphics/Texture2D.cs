using System.Drawing;
using System.Numerics;
using SDL3;
using Smash.EntityComponentSystem;

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
    public readonly nint Handle;

    /// <summary>
    /// The name of the texture gotten from the actual filename without the extension
    /// </summary>
    public readonly string TextureName;

    public Rectangle SourceRectangle => new Rectangle(_sourceRectangle.X, _sourceRectangle.Y, _sourceRectangle.W, _sourceRectangle.H);

    internal readonly SDL.FRect _sourceRectangle;

    internal Color _modulatedColor;

    public Texture2D(nint textureHandle, string textureName)
    {
        Handle = textureHandle;
        SDL.GetTextureSize(Handle, out float width, out float height);
        Width = width;
        Height = height;
        TextureName = textureName;
        _sourceRectangle = new SDL.FRect { X = 0, Y = 0, W = width, H = height};
    }

    internal Texture2D(nint textureHandle, string textureName, Rectangle sourceRectangle)
    {
        Handle = textureHandle;
        Width = sourceRectangle.Width;
        Height = sourceRectangle.Height;
        TextureName = textureName;
        _sourceRectangle = sourceRectangle.ToSDLFRect();
    }

    public Vector2 GetCenter(TransformComponent transformComponent)
    {
        return transformComponent.Position + (Bounds * transformComponent.Scale / 2);
    }
}