using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        public static FloatRect ViewSize = new FloatRect(0, 0, 800, 800);
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(800, 800), "Invaders"))
            {
                window.SetView(new View(ViewSize));
                window.Closed += (_o, e) => window.Close();
                
                MouseHelper.Window = window;
                Clock clock = new Clock();
                SceneManager sceneManager = new SceneManager(window);

                while (window.IsOpen)
                {
                    // Update
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds(); 
                    deltaTime = MathF.Min(deltaTime, 0.01f); // Clamped to prevent collision failing under heavy lag
                    MouseHelper.Update();
                    sceneManager.Update(deltaTime);

                    // Draw
                    window.Clear(new Color(42, 45, 51));
                    sceneManager.Render(window);
                    window.Display();
                }
            }
        }
    }
}