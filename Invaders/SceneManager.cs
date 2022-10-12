using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
            Window = window;
            Events = new EventManager();
            Events.ChangeToScene += ChangeScene;
            
            Scenes = new Dictionary<string, Scene>()
            {
                {"MainMenu", new MainMenu(this)},
                {"NameInput", new NameInput(this, window)},
                {"GamePlay", new GamePlay(this)},
                {"GameOver", new GameOver(this)},
                {"HighScore", new HighScore(this)},
            };
            Scenes.TryGetValue("MainMenu", out currentScene); // Starting scene
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