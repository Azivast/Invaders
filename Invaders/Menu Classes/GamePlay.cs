using System.Collections.Generic;
using System.Data;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class GamePlay: Scene
    {
        private readonly Stats gui;

        public GamePlay(SceneManager sceneManager) : base(sceneManager)
        {
            // Events
            Events.GameOver += GameOver;

            // Spawn Player
            PlayerShip playerShip = new PlayerShip();
            playerShip.Position = new Vector2f(Program.ViewSize.Width/2, Program.ViewSize.Height/2);
            Spawn(playerShip); // TODO: Virtual member call in constructor
            
            // Create gui
            gui = new Stats(playerShip, this);
            
            // Add an enemy spawner
            EnemySpawner spawner = new EnemySpawner();
            spawner.Create(this);
            entities.Add(spawner);
        }
        
        private void GameOver()
        {
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