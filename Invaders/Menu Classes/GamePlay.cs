using System;
using System.Collections.Generic;
using System.Data;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class GamePlay: Scene
    {
        private Stats gui;

        public GamePlay(SceneManager sceneManager) : base(sceneManager)
        {
            // Events
            Events.GameOver += GameOver;
        }

        public override void LoadScene(RenderWindow window)
        {
            // Spawn Player
            PlayerShip playerShip = new PlayerShip();
            playerShip.Position = new Vector2f(Program.ViewSize.Width/2, Program.ViewSize.Height/2);
            Spawn(playerShip);
            
            // Create gui
            gui = new Stats(playerShip, this);
            
            // Add an enemy spawner
            Spawn(new EnemySpawner());
        }

        private void GameOver() // TODO: This gets called twice???
        {
            Console.WriteLine("\nGameOver() in GamePlay.cs called\n");
            Clear();
            Events.PublishChangeScene("GameOver");
        }

        public override void UpdateAll(float deltaTime)
        {
            base.UpdateAll(deltaTime);
            gui.Update(Events);
        }

        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);
            // Draw GUI above everything else
            gui.Render(target);
        }
    }
}