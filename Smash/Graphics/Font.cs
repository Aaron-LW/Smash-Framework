using System.Numerics;
using SDL3;

namespace Smash.Graphics;

public class Font
{
    private Dictionary<string, nint> _textToTexture = new Dictionary<string, nint>();
    
    public readonly float PointSize;
    public nint Handle;
    
    internal Font(nint handle, float pointSize)
    {
        Handle = handle;
        PointSize = pointSize;
    }

    public void Free()
    {
        foreach (nint textureHandle in _textToTexture.Values)
        {
            SDL.DestroyTexture(textureHandle);
        } 

        TTF.CloseFont(Handle);
    }

    public Vector2 MeasureString(string text)
    {
        TTF.GetStringSize(Handle, text, (nuint)text.Length, out int width, out int height);
        return new Vector2(width, height);
    }
    
    internal nint GetOrRenderText(string text, nint renderer)
    {
        if (_textToTexture.TryGetValue(text, out nint cachedTextureHandle))
        {
            return cachedTextureHandle; 
        }

        SDL.Color color = new SDL.Color
        {
            R = 255,
            G = 255,
            B = 255,
            A = 255
        };

        nint surfaceHandle = TTF.RenderTextBlended(Handle, text, (nuint)text.Length, color);
        nint textureHandle = SDL.CreateTextureFromSurface(renderer, surfaceHandle);
        
        _textToTexture[text] = textureHandle;
        return textureHandle;
    }
}