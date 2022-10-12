using System;
using System.ComponentModel.Design;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        public static FloatRect ViewSize = new FloatRect(0, 0, 600, 800);
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(600, 800), "Invaders"))
            {
                window.SetView(new View(ViewSize));
                window.Closed += (o, e) => window.Close();
                MouseCollision.Window = window;

                // TODO: Initialize
                Clock clock = new Clock();
                SceneManager sceneManager = new SceneManager(window);

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