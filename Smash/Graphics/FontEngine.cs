using SDL3;

internal static class FontEngine
{
    public static nint Handle;

    private static Dictionary<nint, Dictionary<string, nint>> _fontTextCache = new Dictionary<nint, Dictionary<string, nint>>();

    /// <summary>
    /// Gets or creates the handle to a TTF.TTFText
    /// </summary>
    /// <param name="font"></param>
    /// <param name="text"></param>
    /// <param name="rendererHandle"></param>
    /// <returns>yo mama</returns>
    internal static nint GetTextHandleFromText(Font font, string text, nint rendererHandle)
    {
        _fontTextCache.TryGetValue(font.Handle, out Dictionary<string, nint>? texts);

        if (texts == null)
        {
            _fontTextCache[font.Handle] = new Dictionary<string, nint>();
            goto createText;
        }

        if (texts.ContainsKey(text))
        {
            return _fontTextCache[font.Handle][text];
        }

        createText:
           nint ttftext = TTF.CreateText(Handle, font.Handle, text, 0);
           _fontTextCache[font.Handle][text] = ttftext;
           return ttftext;
    }
}