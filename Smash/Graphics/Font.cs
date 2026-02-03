using System.Numerics;
using SDL3;

public class Font
{
    public nint Handle;
    
    public Font(string fontPath, float pointSize)
    {
        Handle = TTF.OpenFont(fontPath, pointSize);
    }

    public void Free()
    {
        TTF.CloseFont(Handle);
    }
    
    public Vector2 MeasureString(string text)
    {
        int widht, height;

        nint textHandle = TTF.CreateText(FontEngine.Handle, Handle, text, 0);

        TTF.GetTextSize(textHandle, out widht, out height);

        return new Vector2(widht, height);
    }
}