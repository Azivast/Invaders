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
        public readonly EventManager Events;
        public static RenderWindow Window;

        // Constructor
        public SceneManager(RenderWindow window)
        {
            Scenes = new Dictionary<string, Scene>()
            {
                {"MainMenu", new MainMenu(this)},
                {"GamePlay", new GamePlay(this)},
                {"HighScore", new HighScore(this)},
            };
            Scenes.TryGetValue("MainMenu", out currentScene); // Starting scene

            Window = window;
            Events = new EventManager();
            Events.ChangeToScene += ChangeScene;

            
            //Spawn stuff in game scene TODO: Nicer code
            Scenes.TryGetValue("GamePlay", out Scene gamePlay);
            PlayerShip playerShip = new PlayerShip();
            playerShip.Position = new Vector2f(100, 500);
            gamePlay.Spawn(playerShip);
            
            // DEBUG: Spawn some enemies
            EnemyShip enemy = new EnemyShip();
            gamePlay.Spawn(enemy);

        }

        private void ChangeScene(string scene)
        {
            if (scene.Equals("Quit")) 
                Window.Close();
            else
                Scenes.TryGetValue(scene, out currentScene);
            Console.WriteLine(scene);
            Console.WriteLine(currentScene);
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