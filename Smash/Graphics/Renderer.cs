using System.Drawing;
using System.Numerics;
using SDL3;
using Smash.EntityComponentSystem;

namespace Smash.Graphics;

public class Renderer
{
    public nint Handle;
    private Camera? _camera;

    public Renderer(nint rendererHandle)
    {
        Handle = rendererHandle;
    }

    public void Destroy()
    {
        SDL.DestroyRenderer(Handle);
    }

    public void Clear(Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, 1);
        SDL.RenderClear(Handle);
    }

    public void RenderTexture(Texture2D texture, Vector2 position, Color color, float? width = null, float? height = null, float? scale = null)
    {
        SDL.FRect rect = new SDL.FRect
        {
            X = position.X - _camera?.X ?? 0,
            Y = position.Y - _camera?.Y ?? 0,
            W = (width ?? texture.Width) * (scale ?? 1),
            H = (height ?? texture.Height) * (scale ?? 1)
        };

        SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
        SDL.RenderTexture(Handle, texture.Handle, IntPtr.Zero, rect);
    }

    public void RenderEntity(Entity entity, Color color)
    {
        TransformComponent? transform = entity.GetComponent<TransformComponent>();
        Texture2D? texture = entity.GetComponent<TextureComponent>()?.Texture;

        if (transform == null) throw new InvalidOperationException("Entity of type " + entity.GetType() + " has no transform and thus cant be drawn");
        if (texture == null) throw new InvalidOperationException("Entity of type " + entity.GetType() + " has no Texture and thus cant be drawn");

        SDL.FRect rect = new SDL.FRect
        {
            X = transform.X - _camera?.X ?? 0,
            Y = transform.Y - _camera?.Y ?? 0,
            W = texture.Width * transform.Scale,
            H = texture.Height * transform.Scale
        };

        SDL.SetTextureColorMod(texture.Handle, color.R, color.G, color.B);
        SDL.RenderTexture(Handle, texture.Handle, IntPtr.Zero, rect);
    }

    public void RenderLine(float x1, float y1, float x2, float y2, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.RenderLine(Handle, x1, y1, x2, y2);
    }

    public void RenderRectangleWireframe(Rectangle rectangle, Color color)
    {
        RenderLine(rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, color);
        RenderLine(rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y, color);
        RenderLine(rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + rectangle.Height, color);
        RenderLine(rectangle.X, rectangle.Y + rectangle.Height, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, color);
        RenderLine(rectangle.X + rectangle.Width, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Width, color);
    }

    public void RenderFilledRectangle(Rectangle rectangle, Color color)
    {
        SDL.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A);
        SDL.FRect rect = rectangle.ToSDLFRect();
        rect.X -= _camera?.X ?? 0;
        rect.Y -= _camera?.Y ?? 0;

        SDL.RenderFillRect(Handle, rect);
    }

    public void RenderPresent()
    {
        SDL.RenderPresent(Handle);
    }

    public void SetActiveCamera(Camera camera)
    {
        _camera = camera;
    }
}