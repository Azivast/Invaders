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
            playerShip.Position = new Vector2f(100, 500);
            Spawn(playerShip); // TODO: Virtual member call in constructor
            
            // Create gui
            gui = new Stats(playerShip, this);
            
            // DEBUG: Spawn some enemies
            EnemyShip enemy = new EnemyShip();
            Spawn(enemy);
        }
        
        private void GameOver()
        {
            Clear();
            Events.PublishChangeSceneEvent("GameOver");
        }

        public override void UpdateAll(float deltaTime)
        {
            base.UpdateAll(deltaTime);
            gui.Update(Events);
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) Spawn(new EnemyShip()); // DEBUG
        }

        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);
            // Draw GUI above everything else
            gui.Render(target);
        }
    }
}