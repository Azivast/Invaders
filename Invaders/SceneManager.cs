using System;
using SFML.Graphics;

namespace Invaders
{
    public class SceneManager
    {
        private const Scenes StartingScene = Scenes.Game;
        // TODO: Maybe use a list instead?
        public enum Scenes
        {
            MainMenu,
            Game,
            HighScore,
            Quit
        }
        private Scenes currentScene;
        
        // Constructor
        public SceneManager()
        {
            currentScene = StartingScene;
        }
        
        // Update
        public void Update(float deltaTime)
        {
            switch (currentScene)
            {
                case Scenes.Game:
                    break;
                default:
                    throw new Exception($"scene '{currentScene}' does not exist.");
            }
        }
        
        //Draw
        public void Render(RenderTarget target)
        {
            switch (currentScene)
            {
                case Scenes.Game:
                    break;
                default:
                    throw new Exception($"scene '{currentScene}' does not exist.");
            }
        }
    }
}