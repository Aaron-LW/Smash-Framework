using Smash.Graphics;
using SDL3;

namespace Smash.Graphics;

public static class AssetManager
{
    private static Dictionary<string, Texture2D> _loadedTextures = new();
    private static Dictionary<string, FontSystem> _loadedFontSystems = new();

    private static string _rootDirectoryPath = "";
    private static BlendMode _defaultBlendMode = BlendMode.Blend;
    private static ScaleMode _defaultScaleMode = ScaleMode.Linear;

    /// <summary>
    /// Tries to get a texture from the already loaded textures
    /// </summary>
    public static Texture2D? TryGetTexture(string textureName)
    {
        _loadedTextures.TryGetValue(textureName, out Texture2D? texture);
        return texture;
    }

    /// <summary>
    /// Gets a texture from the already loaded textures by direct access.
    /// Will crash if the texture doesn't exist
    /// </summary>
    public static Texture2D GetTexture(string textureName)
    {
        return _loadedTextures[textureName];
    }

    /// <summary>
    /// Tries to get a font from the already loaded fonts
    /// </summary>
    public static Font? TryGetFont(string fontName, float pointSize)
    {
        if (_loadedFontSystems.TryGetValue(fontName, out FontSystem? fontSystem))
        {
            Font font = fontSystem.GetOrCreateFont(pointSize);
            return font;
        }
        
        return null;
    }

    /// <summary>
    /// Gets a font from the already loaded fonts by direct access.
    /// Will crash if the font doesn't exist
    /// </summary>
    public static Font GetFont(string fontName, float pointSize)
    {
        FontSystem fontSystem = _loadedFontSystems[fontName];
        Font font = fontSystem.GetOrCreateFont(pointSize);
        return font;
    }

    /// <summary>
    /// Sets the relative path from which all assets will be loaded
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void SetAssetRootDirectory(string directoryPath)
    {
        _rootDirectoryPath = directoryPath;
    }

    /// <summary>
    /// Sets the default blend mode that should be applied to every loaded Texture2D after this function has been called
    /// </summary>
    public static void SetDefaultBlendMode(BlendMode blendMode)
    {
        _defaultBlendMode = blendMode;
    }

    /// <summary>
    /// Sets the default scale mode that should be applied to every loaded Texture2D after this function has been called
    /// </summary>
    public static void SetDefaultScaleMode(ScaleMode scaleMode)
    {
        _defaultScaleMode = scaleMode;
    }

    /// <summary>
    /// Loads a Texture2D from an image relative to the root directory path
    /// The loaded Texture2D will be stored into the _loadedTextures dictionary with the key being the files name without the extension
    /// </summary>
    /// <returns>The loaded Texture2D</returns>
    public static Texture2D LoadTexture(string relativePath, Renderer renderer)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, relativePath);
        string fileName = Path.GetFileNameWithoutExtension(fullPath);

        if (!File.Exists(fullPath)) throw new FileNotFoundException($"File at {fullPath} could not be found");

        nint textureHandle = Image.LoadTexture(renderer.Handle, fullPath);
        Texture2D texture = new Texture2D(textureHandle, fileName);

        SDL.SetTextureBlendMode(texture.Handle, (SDL.BlendMode)_defaultBlendMode);
        SDL.SetTextureScaleMode(texture.Handle, (SDL.ScaleMode)_defaultScaleMode);

        _loadedTextures.Add(fileName, texture);
        return texture;
    }

    public static Texture2D AddTextureRegion(string name, TextureRegion textureRegion)
    {
        _loadedTextures.TryGetValue(textureRegion.BaseTextureName, out Texture2D? baseTexture);
        if (baseTexture == null) throw new Exception($"""Texture with name "{name}" cannot be found""");

        Texture2D texture = new Texture2D(baseTexture.Handle, name, new Rectangle(textureRegion.X, textureRegion.Y, textureRegion.Width, textureRegion.Height));
        _loadedTextures.Add(name, texture);

        return texture;
    }

    /// <summary>
    /// Loads a Font from a .ttf file relative to the root directory path
    /// This font does not get stored internally
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    public static void LoadFont(string relativePath)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, relativePath);
        string fontName = Path.GetFileNameWithoutExtension(fullPath);

        if (!File.Exists(fullPath)) throw new FileNotFoundException($"Font at {fullPath} could not be found");

        FontSystem fontSystem = new FontSystem(fullPath);

        _loadedFontSystems.Add(fontName, fontSystem);
    }

    public static void Dispose()
    {
        foreach (FontSystem fontSystem in _loadedFontSystems.Values)
        {
            fontSystem.Dispose();
        }

        foreach (Texture2D texture in _loadedTextures.Values)
        {
            texture.Dispose();
        }

        _loadedFontSystems.Clear();
        _loadedTextures.Clear();
    }
}