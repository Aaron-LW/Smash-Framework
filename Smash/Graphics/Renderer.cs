using System.Drawing;
using System.Numerics;
using SDL3;
using Smash.EntityComponentSystem;

namespace Smash.Graphics;

public class Renderer
{
    /// <summary>
    /// The handle of this instance of the Renderer class
    /// </summary>
    public nint Handle;

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
    public void RenderTexture(Texture2D texture, Vector2 position, Color color, float scale = 1)
    {
        SDL.FRect dstRect = new SDL.FRect
        {
            X = position.X,
            Y = position.Y,
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        if (texture._modulatedColor.R != color.R ||
            texture._modulatedColor.G != color.G ||
            texture._modulatedColor.B != color.B ||
            texture._modulatedColor.A != color.A)
        {
            SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
            SDL.SetTextureAlphaMod(texture.Handle, color.A);
            texture._modulatedColor = color;
        }

        SDL.RenderTexture(Handle, texture.Handle, texture._sourceRectangle, dstRect);
    }

    /// <summary>
    ///Renders a Texture to the screen
    /// </summary>
    /// <param name="texture">The Texture2D to be drawn to the screen</param>
    /// <param name="x">The x position where the texture should be drawn at</param>
    /// <param name="y">The y position where the texture should be drawn at </param>
    /// <param name="color">The color which the texture should have</param>
    /// <param name="width">The width the texture will be scaled to</param>
    /// <param name="height">The height the texture will be scaled to</param>
    /// <param name="scale">The scale which the textures bounds should be multiplied with</param>
    public void RenderTexture(Texture2D texture, float x, float y, Color color, float scale = 1)
    {
        SDL.FRect dstRect = new SDL.FRect
        {
            X = x,
            Y = y,
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        if (texture._modulatedColor.R != color.R ||
            texture._modulatedColor.G != color.G ||
            texture._modulatedColor.B != color.B ||
            texture._modulatedColor.A != color.A)
        {
            SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
            SDL.SetTextureAlphaMod(texture.Handle, color.A);
            texture._modulatedColor = color;
        }

        SDL.RenderTexture(Handle, texture.Handle, texture._sourceRectangle, dstRect);
    }

    public void RenderTextureRotated(Texture2D texture, Vector2 position, Color color, double angle, float scale = 1)
    {
        SDL.FRect rect = new SDL.FRect
        {
            X = position.X,
            Y = position.Y,
            W = texture.Width * scale,
            H = texture.Height * scale
        };

        if (texture._modulatedColor.R != color.R ||
            texture._modulatedColor.G != color.G ||
            texture._modulatedColor.B != color.B ||
            texture._modulatedColor.A != color.A)
        {
            SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
            SDL.SetTextureAlphaMod(texture.Handle, color.A);
            texture._modulatedColor = color;
        }

        SDL.RenderTextureRotated(Handle, texture.Handle, texture._sourceRectangle, rect, angle, IntPtr.Zero, SDL.FlipMode.None);
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
            X = transform.X,
            Y = transform.Y,
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
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderLine(Handle, x1, y1, x2, y2);
    }

    public void RenderLine(Vector2 startPosition, Vector2 endPosition, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderLine(Handle, startPosition.X, startPosition.Y, endPosition.X, endPosition.Y);
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

        SDL.RenderFillRect(Handle, rect);
    }

    /// <summary>
    /// Renders text to the screen at specified position
    /// </summary>
    /// <param name="font">The Font of the text</param>
    /// <param name="text">The string representing the text that should be drawn</param>
    /// <param name="position">The position where the text should be drawn in screen coordinates</param>
    /// <param name="color">The color which the text should be</param>
    public void RenderText(Font font, string text, Vector2 position, Color color)
    {
        nint textTextureHandle = font.GetOrRenderText(text, Handle);
        
        SDL.GetTextureSize(textTextureHandle, out float width, out float height);
        SDL.FRect rect = new SDL.FRect
        {
            X = position.X,
            Y = position.Y,
            W = width,
            H = height
        };

        SDL.SetTextureColorMod(textTextureHandle, color.R, color.G, color.B);
        SDL.SetTextureAlphaMod(textTextureHandle, color.A);
        SDL.RenderTexture(Handle, textTextureHandle, IntPtr.Zero, rect);
    }

    public void SetRenderBlendMode(BlendMode blendMode)
    {
        SDL.SetRenderDrawBlendMode(Handle, (SDL.BlendMode)blendMode);
    }

    /// <summary>
    /// Enables or disables vsync for this renderer
    /// </summary>
    /// <param name="enabled">If vsync should be enabled or not</param>
    public void SetVSyncEnabled(bool enabled)
    {
        SDL.SetRenderVSync(Handle, enabled ? 1 : 0);
    }

    /// <summary>
    /// Renders everything to the screen
    /// </summary>
    public void RenderPresent()
    {
        SDL.RenderPresent(Handle);
    }
}