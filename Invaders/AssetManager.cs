using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;

namespace Invaders
{
    public class AssetManager
    {
        public static readonly string AssetPath = "assets";
        private readonly Dictionary<string, Texture> textures;
        private readonly Dictionary<string, Font> fonts;
        private readonly Dictionary<string, SoundBuffer> sounds;


        public AssetManager()
        {
            textures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
            sounds = new Dictionary<string, SoundBuffer>();
        }
        
        public Texture LoadTexture(string name)
        {
            // Return texture if it's already loaded.
            if (textures.TryGetValue(name, out Texture found)) 
                return found;

            
            // Otherwise load it and return.
            string fileName = $"assets/textures/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);

            return texture;
        }

        public Font LoadFont(string pixelFont)
        {
            // Return font if it's already loaded.
            if (fonts.TryGetValue(pixelFont, out Font found)) 
                return found;

            
            // Otherwise load it and return.
            string fileName = $"assets/fonts/{pixelFont}.ttf";
            Font font = new Font(fileName);
            fonts.Add(pixelFont, font);

            return font;
        }
        
        public SoundBuffer LoadSoundBuffer(string sound)
        {
            // Return font if it's already loaded.
            if (sounds.TryGetValue(sound, out SoundBuffer found)) 
                return found;

            
            // Otherwise load it and return.
            string fileName = $"assets/sounds/{sound}.ogg";
            SoundBuffer buffer = new SoundBuffer(fileName);
            sounds.Add(sound, buffer);

            return buffer;
        }
    }
}