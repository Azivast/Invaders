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
        public readonly Dictionary<string, Scene> Scenes;
        private Scene currentScene;
        public readonly EventManager Events;
        public static RenderWindow Window;
        Background background;

        // Constructor
        public SceneManager(RenderWindow window)
        {
            Window = window;
            Events = new EventManager();
            Events.ChangeToScene += ChangeScene;
            background = new Background(window);
            
            Scenes = new Dictionary<string, Scene>()
            {
                {"MainMenu", new MainMenu(this, window)},
                {"GamePlay", new GamePlay(this)},
                {"GameOver", new GameOver(this, window)},
                {"HighScore", new HighScore(this, window)},
            };
            ChangeScene("MainMenu");  // Starting scene
        }

        private void ChangeScene(string scene)
        {
            if (scene.Equals("Quit")) 
                Window.Close();
            else
            {
                foreach (var item in Scenes)
                {
                    if (item.Key.Equals(scene))
                    {
                        item.Value.LoadScene(Window);
                        Scenes.TryGetValue(scene, out currentScene);
                    }
                    else
                    {
                        item.Value.UnLoadScene(Window);
                    }
                }

            }
        }
        
        // Update
        public void Update(float deltaTime)
        {
            currentScene.UpdateAll(deltaTime);
            background.Update(deltaTime);
        }
        
        //Draw
        public void Render(RenderTarget target)
        {
            background.Render(target);
            currentScene.RenderAll(target);
        }
    }
}