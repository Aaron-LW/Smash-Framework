using SDL3;

internal class FontSystem
{
    public readonly string BaseFontPath;
    private Dictionary<float, Font> _fonts = new();

    public FontSystem(string baseFontPath)
    {
        BaseFontPath = baseFontPath;
    }

    public Font GetOrCreateFont(float pointSize)
    {
        if (_fonts.TryGetValue(pointSize, out Font? alreadyCreatedFont))
        {
            return alreadyCreatedFont;
        }

        nint fontHandle = TTF.OpenFont(BaseFontPath, pointSize);
        Font font = new Font(fontHandle, pointSize);
        _fonts.Add(pointSize, font);
        return font;
    }
}