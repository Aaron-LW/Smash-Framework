using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using SDL3;
using Smash.EntityComponentSystem;

namespace Smash.Graphics;

public class Renderer
{
    /// <summary>
    /// A boolean representing if the offset vector should be used when rendering
    /// </summary>
    public bool OffsetVectorEnabled = true;

    /// <summary>
    /// The handle of this instance of the Renderer class
    /// </summary>
    public nint Handle;

    public Vector2 OffsetVector { get; private set; }
    private float _zoom;

    public Renderer(nint rendererHandle)
    {
        Handle = rendererHandle;
    }

    /// <summary>
    /// Destroys this renderer
    /// </summary>
    public void Destroy()
    {
        SDL.DestroyRenderer(Handle);
    }

    /// <summary>
    /// Clears the screen with the specified color
    /// </summary>
    /// <param name="color">The color which the screen should be set to</param>
    public void Clear(Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, 1);
        SDL.RenderClear(Handle);
    }

    /// <summary>
    ///Renders a Texture to the screen
    /// </summary>
    /// <param name="texture">The Texture2D to be drawn to the screen</param>
    /// <param name="position">The position where the texture should be drawn at</param>
    /// <param name="color">The color which the texture should have</param>
    /// <param name="width">The width the texture will be scaled to</param>
    /// <param name="height">The height the texture will be scaled to</param>
    /// <param name="scale">The scale which the textures bounds should be multiplied with</param>
    public void RenderTexture(Texture2D texture, Vector2 position, Color color, float? width = null, float? height = null, float? scale = null)
    {
        SDL.FRect rect = new SDL.FRect
        {
            X = position.X - (OffsetVectorEnabled ? OffsetVector.X : 0),
            Y = position.Y - (OffsetVectorEnabled ? OffsetVector.Y : 0),
            W = (width ?? texture.Width) * (scale ?? 1),
            H = (height ?? texture.Height) * (scale ?? 1)
        };

        SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
        SDL.RenderTexture(Handle, texture.Handle, IntPtr.Zero, rect);
    }

    public void RenderTextureRotated(Texture2D texture, Vector2 position, Color color, double angle, float scale = 1)
    {
        SDL.FRect rect = new SDL.FRect
        {
            X = position.X - (OffsetVectorEnabled ? OffsetVector.X : 0),
            Y = position.Y - (OffsetVectorEnabled ? OffsetVector.Y : 0),
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
        SDL.RenderTextureRotated(Handle, texture.Handle, IntPtr.Zero, rect, angle, IntPtr.Zero, SDL.FlipMode.None);
    }

    /// Renders an entity to the screen if it has a Transform- and TextureComponent
    /// </summary>
    /// <param name="entity">The entity to be drawn</param>
    /// <param name="color">The color of the entities texture</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void RenderEntity(Entity entity, Color color)
    {
        TransformComponent? transform = entity.GetComponent<TransformComponent>();
        Texture2D? texture = entity.GetComponent<TextureComponent>()?.Texture;

        if (transform == null) throw new InvalidOperationException("Entity of type " + entity.GetType() + " has no transform and thus cant be drawn");
        if (texture == null) throw new InvalidOperationException("Entity of type " + entity.GetType() + " has no Texture and thus cant be drawn");

        SDL.FRect rect = new SDL.FRect
        {
            X = transform.X - (OffsetVectorEnabled ? OffsetVector.X : 0),
            Y = transform.Y - (OffsetVectorEnabled ? OffsetVector.Y : 0),
            W = texture.Width * transform.Scale,
            H = texture.Height * transform.Scale
        };

        SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
        SDL.RenderTexture(Handle, texture.Handle, IntPtr.Zero, rect);
    }

    /// <summary>
    /// Renders a line between two points
    /// </summary>
    /// <param name="color">The color of the line</param>
    public void RenderLine(float x1, float y1, float x2, float y2, Color color)
    {
        x1 -= OffsetVectorEnabled ? OffsetVector.X : 0;
        x2 -= OffsetVectorEnabled ? OffsetVector.X : 0;
        y1 -= OffsetVectorEnabled ? OffsetVector.Y : 0;
        y2 -= OffsetVectorEnabled ? OffsetVector.Y : 0;

        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderLine(Handle, x1, y1, x2, y2);
    }

    /// <summary>
    /// Renders a non-filled rectangle
    /// </summary>
    /// <param name="rectangle">The rectangle to be drawn</param>
    /// <param name="color">The color the rectangle should have</param>
    public void RenderRectangle(Rectangle rectangle, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.FRect rect = rectangle.ToSDLFRect();

        rect.X -= OffsetVectorEnabled ? OffsetVector.X : 0;
        rect.Y -= OffsetVectorEnabled ? OffsetVector.Y : 0;

        SDL.RenderRect(Handle, rect);
    }

    /// <summary>
    /// Renders a filled rectangle with the specified color
    /// </summary>
    /// <param name="rectangle">The rectangle to be drawn</param>
    /// <param name="color">The color the rectangle should appear as</param>
    public void RenderFilledRectangle(Rectangle rectangle, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.FRect rect = rectangle.ToSDLFRect();

        rect.X -= OffsetVectorEnabled ? OffsetVector.X : 0;
        rect.Y -= OffsetVectorEnabled ? OffsetVector.Y : 0;

        SDL.RenderFillRect(Handle, rect);
    }

    public void RenderText(Font font, string text, Vector2 position, Color color)
    {
        nint textHandle = FontEngine.GetTextHandleFromText(font, text, Handle);
        TTF.SetTextColor(textHandle, color.R, color.G, color.B, color.A);

        TTF.DrawRendererText(textHandle, position.X, position.Y);
    }

    /// <summary>
    /// Renders everything to the screen
    /// </summary>
    public void RenderPresent()
    {
        SDL.RenderPresent(Handle);
    }

    public void SetOffsetVector(Vector2 offsetVector)
    {
        OffsetVector = offsetVector;
    }
}