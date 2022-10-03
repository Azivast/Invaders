using System;
using System.ComponentModel.Design;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(400, 800), "Invaders"))
            {
                window.SetView(new View(new FloatRect(0, 0 , 400, 800)));
                window.Closed += (o, e) => window.Close();
                
                // TODO: Initialize
                Clock clock = new Clock();
                SceneManager sceneManager = new SceneManager();

                while (window.IsOpen)
                {
                    window.DispatchEvents();

                    // Update
                    float deltaTime = clock.Restart().AsSeconds();
                    // TODO: Clamp delta to prevent collision clipping?
                    sceneManager.Update(deltaTime);
                    
                    // Draw
                    window.Clear(new Color(50, 50, 60));
                    sceneManager.Render(window);
                    window.Display();
                }
            }
        }
    }
}