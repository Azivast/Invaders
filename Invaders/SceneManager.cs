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

        // Constructor
        public SceneManager(RenderWindow window)
        {
            Window = window;
            Events = new EventManager();
            Events.ChangeToScene += ChangeScene;
            
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
            Console.WriteLine("-----start-----");
            Console.WriteLine(scene + " is the scene to change to \n");
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
            Console.WriteLine("-----stop------");
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