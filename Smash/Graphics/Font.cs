using SDL3;

namespace Smash.Graphics;

public class Font
{
    private Dictionary<string, nint> _textToTexture = new Dictionary<string, nint>();

    public nint Handle;
    
    public Font(string fontPath, float pointSize)
    {
        Handle = TTF.OpenFont(fontPath, pointSize);

        if (Handle == 0)
        {
            throw new FileNotFoundException("Cannot find Font at " + fontPath);
        }
    }

    public void Free()
    {
        foreach (nint textureHandle in _textToTexture.Values)
        {
            SDL.DestroyTexture(textureHandle);
        } 

        TTF.CloseFont(Handle);
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