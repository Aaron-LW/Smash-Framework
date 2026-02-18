using SDL3;

namespace Smash.Graphics;

public class TextureLoader
{
    /// <summary>
    /// All loaded textures across all instances of the TextureLoader
    /// </summary>
    public static Dictionary<string, Texture2D> LoadedTextures { get; private set; } = new Dictionary<string, Texture2D>();

    /// <summary>
    /// Gets the Texture2D with the specified name
    /// </summary>
    /// <param name="fileName">The name of the file without the extension</param>
    /// <returns>The Texture2D that corresponds to the fileName parameter</returns>
    public static Texture2D Get(string fileName)
    {
        return LoadedTextures[fileName];
    }

    public static Texture2D? TryGet(string fileName)
    {
        LoadedTextures.TryGetValue(fileName, out Texture2D? texture);
        return texture;
    }

    /// <summary>
    /// Loads all images from a specified directory
    /// </summary>
    /// <param name="renderer">A renderer</param>
    /// <param name="path">The path to the directory of images</param>
    public void LoadDirectory(Renderer renderer, string path)
    {
        nint rendererHandle = renderer.Handle;
        string[] filePaths = Directory.GetFileSystemEntries(path);
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            nint surfaceHandle = Image.Load(filePath);

            nint textureHandle = SDL.CreateTextureFromSurface(rendererHandle, surfaceHandle);

            SDL.DestroySurface(surfaceHandle);

            SDL.SetTextureScaleMode(textureHandle, SDL.ScaleMode.Nearest);

            LoadedTextures[fileName] = new Texture2D(textureHandle, fileName);
        }
    }

    /// <summary>
    /// Destroys all loaded textures from every TextureLoader
    /// </summary>
    public void DestroyTextures()
    {
        foreach (Texture2D texture in LoadedTextures.Values) SDL.DestroyTexture(texture.Handle);
    }
}