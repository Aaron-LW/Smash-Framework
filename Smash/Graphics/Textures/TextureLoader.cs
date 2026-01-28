using SDL3;

namespace Smash.Graphics;

public class TextureLoader
{
    public static Dictionary<string, Texture2D> LoadedTextures { get; private set; } = new Dictionary<string, Texture2D>();

    public static Texture2D Get(string fileName)
    {
        return LoadedTextures[fileName];
    }

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

            LoadedTextures[fileName] = new Texture2D(textureHandle);
        }
    }

    public void DestroyTextures()
    {
        foreach (Texture2D texture in LoadedTextures.Values) SDL.DestroyTexture(texture.Handle);
    }
}