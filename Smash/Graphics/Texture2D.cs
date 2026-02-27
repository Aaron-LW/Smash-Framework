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
    public nint Handle;

    /// <summary>
    /// The name of the texture gotten from the actual filename without the extension
    /// </summary>
    public string TextureName;

    public Texture2D(nint textureHandle, string textureName)
    {
        Handle = textureHandle;
        SDL.GetTextureSize(Handle, out float width, out float height);
        Width = width;
        Height = height;
        TextureName = textureName;
    }

    public Vector2 GetCenter(TransformComponent transformComponent)
    {
        return transformComponent.Position + (Bounds * transformComponent.Scale / 2);
    }
}