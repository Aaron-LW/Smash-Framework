using System.Drawing;
using SDL3;

public class Font
{
    public nint Handle;
    
    private Dictionary<string, nint> _stringTextures = new Dictionary<string, nint>();

    public Font(nint handle)
    {
        Handle = handle;
    }

    public void Free()
    {
        TTF.CloseFont(Handle);
    }

    internal nint GetTextureHandleFromText(nint rendererHandle, string text)
    {
        if (_stringTextures.TryGetValue(text, out nint textureHandle))
        {
            return textureHandle;
        }

        SDL.Color white = new SDL.Color
        {
            R = 0,
            G = 0,
            B = 0,
            A = 1
        };

        nint surface = TTF.RenderTextBlended(Handle, text, 0, white);
        nint texture = SDL.CreateTextureFromSurface(rendererHandle, surface);
    
        _stringTextures[text] = texture;

        SDL.Free(surface);
        return texture;
    }
}