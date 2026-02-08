using SDL3;

public class Font
{
    private Dictionary<string, nint> _textToTexture = new Dictionary<string, nint>();

    public nint Handle;
    
    public Font(string fontPath, float pointSize)
    {
        Handle = TTF.OpenFont(fontPath, pointSize);
    }

    public void Free()
    {
        foreach (nint textureHandle in _textToTexture.Values)
        {
            SDL.DestroyTexture(textureHandle);
        } 

        TTF.CloseFont(Handle);
    }
    
    public nint GetOrRenderText(string text)
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

        nint textureHandle = TTF.RenderTextBlended(Handle, text, (nuint)text.Length, color);
        _textToTexture[text] = textureHandle;
        return textureHandle;
    }
}