using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class SceneManager
    {
        // TODO: Maybe use a list instead?
        public readonly Dictionary<string, Scene> Scenes;
        private Scene currentScene;

        // Constructor
        public SceneManager()
        {
            Scenes = new Dictionary<string, Scene>()
            {
                {"MainMenu", new Scene()},
                {"Game", new Scene()},
                {"HighScore", new Scene()},
                {"Quit", new Scene()},
            };
            
            
            Scenes.TryGetValue("Game", out currentScene); // Set current scene to game
            // Spawn stuff
            PlayerShip playerShip = new PlayerShip();
            playerShip.Position = new Vector2f(100, 500);
            currentScene.Spawn(playerShip);
            
            // DEBUG: Spawn some enemies
            EnemyShip enemy = new EnemyShip();
            currentScene.Spawn(enemy);
        }
        
        // Update
        public void Update(float deltaTime)
        {
            currentScene.UpdateAll(deltaTime);
            
        }
        
        //Draw
        public void Render(RenderTarget target)
        {
            currentScene.RenderAll(target);
        }
    }
}