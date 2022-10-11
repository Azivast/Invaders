using System.Collections.Generic;
using SFML.Graphics;

namespace Invaders
{
    public class GamePlay: Scene
    {
        private readonly Stats gui;

        public GamePlay(SceneManager sceneManager) : base(sceneManager)
        {
            gui = new Stats(this);
        }

        public override void Spawn(Entity entity)
        {
            base.Spawn(entity);
            
            //TODO: Implement differently so that check wont run every time.
            if (entity is PlayerShip ship) gui.LoadPlayerShip(ship);
        }

        public override void UpdateAll(float deltaTime)
        {
            base.UpdateAll(deltaTime);
            gui.Update();
        }

        public override void RenderAll(RenderTarget target)
        {
            base.RenderAll(target);
            // Draw GUI above everything else
            gui.Render(target);
        }
    }
}